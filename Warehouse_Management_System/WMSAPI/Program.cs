using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;
using WarehouseMSAPI.DTO;
using WarehouseMSAPI.Mapper;
using WMSAPI.DTO;
using WMSAPI.TokenRepository;

static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new();
    builder.EntitySet<RoleDTO>("Roles");
    builder.EntitySet<CategoryDTO>("Categories");
    builder.EntitySet<SupplierDTO>("Suppliers");
    builder.EntitySet<CustomerDTO>("Customers");
    builder.EntitySet<CarrierDTO>("Carriers");
    builder.EntitySet<LocationDTO>("Locations");
    builder.EntitySet<UserDTO>("Users");
    builder.EntitySet<ProductDTO>("Products");
    builder.EntitySet<InventoryDTO>("Inventories");
    builder.EntitySet<TransactionDTO>("Transactions");
    builder.EntitySet<ReportDTO>("Reports");
    return builder.GetEdmModel();
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// accept cycle when json serializes
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

//builder.Services.AddDbContext<MyDbContext>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();

builder.Services.AddControllers()
    .AddOData(options => options
        .AddRouteComponents("odata", GetEdmModel())
        .Select()
        .Filter()
        .OrderBy()
        .SetMaxTop(100)
        .Count()
        .Expand()
    );

//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddAutoMapper(typeof("name class config"));
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = $"JWT Authorization header using the Bearer scheme. \r\n\r\n" +
                      "Enter 'Bearer'[space] and then your token in the text input below." +
                      "\r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
            Reference = new OpenApiReference
                {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
            }
    });
});

// config authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    var Key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        //ValidateIssuer = false,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Key)
    };
});

// config accept ajax
builder.Services.AddCors(options =>
{
    options.AddPolicy("Origin",
        builder =>
        {
            builder
            //.WithOrigins("https://localhost:'specific url side client'")
            .AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// setting countToken, baseToken
var json = File.ReadAllText("appsettings.json");
dynamic config = JsonConvert.DeserializeObject(json);
config.JWT.CountToken = 0;
config.JWT.BaseAccessToken = "";
var newJson = JsonConvert.SerializeObject(config);
File.WriteAllText("appsettings.json", newJson);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseODataBatching();

app.UseRouting();

app.UseCors("Origin");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
