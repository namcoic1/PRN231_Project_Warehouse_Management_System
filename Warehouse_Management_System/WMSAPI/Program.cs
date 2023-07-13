using AutoMapper;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using WarehouseMSAPI.DTO;
using WarehouseMSAPI.Mapper;
using WMSAPI.DTO;

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

//builder.Services.AddDbContext<MyDbContext>(options =>
//{
//    //options.UseSqlServer(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("Database"));
//    //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
//    //options.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
//});
//builder.Services.AddDbContext<MyDbContext>();
//builder.Services.AddScoped<MyDbContext>();

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseODataBatching();

app.UseRouting();

app.UseCors("Origin");

app.Run();
