using AutoMapper;
using BusinessObjects;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Repositories.UserRepo;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WMSAPI.DTO;

namespace WMSAPI.TokenRepository
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository repository;
        private readonly IMapper Mapper;
        private int countToken;
        public TokenRepository(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            repository = new UserRepository();
            Mapper = mapper;
        }

        public TokenDTO Authenticate(User user)
        {
            var _user = repository.GetUserByLogin(user.UserName, user.Password);

            // check user
            if (_user == null)
            {
                return new TokenDTO();
            }

            // count token gen
            countToken = _configuration.GetValue<int>("JWT:CountToken");
            countToken++;

            // gen JSON token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"],
                Subject = new ClaimsIdentity(new Claim[] {
                    // user id
                    new Claim("UserId", _user.Id.ToString()),
                    // user name
                    new Claim(ClaimTypes.Name, _user.UserName),
                    // role user
                    new Claim(ClaimTypes.Role, _user.Role.Name),

                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["JWT:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    // json user
                    new Claim("User", System.Text.Json.JsonSerializer.Serialize<UserDTO>(Mapper.Map<UserDTO>(_user)))
                }),
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(countToken <= 1 ? _configuration["JWT:AccessTokenExpiration"] : _configuration["JWT:RefreshTokenExpiration"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var appSettingsPath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "appsettings.json");
            var json = File.ReadAllText(appSettingsPath);
            var jsonSettings = new JsonSerializerSettings();
            jsonSettings.Converters.Add(new ExpandoObjectConverter());
            jsonSettings.Converters.Add(new StringEnumConverter());
            dynamic config = JsonConvert.DeserializeObject<ExpandoObject>(json, jsonSettings);

            // setting countToken, baseToken
            config.JWT.CountToken = countToken;
            if (countToken <= 1) config.JWT.BaseAccessToken = tokenHandler.WriteToken(token);

            var newJson = JsonConvert.SerializeObject(config, Newtonsoft.Json.Formatting.Indented, jsonSettings);
            File.WriteAllText(appSettingsPath, newJson);

            //return new TokenDTO { Token = "Bearer " + tokenHandler.WriteToken(token) };
            return new TokenDTO
            {
                Token = countToken <= 1 ? tokenHandler.WriteToken(token) : _configuration.GetValue<string>("JWT:BaseAccessToken"),
                RefreshToken = countToken > 1 ? tokenHandler.WriteToken(token) : "",
                ValidTo = token.ValidTo
            };
        }
    }
}
