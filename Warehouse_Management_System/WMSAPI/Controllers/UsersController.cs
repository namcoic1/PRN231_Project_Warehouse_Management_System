using AutoMapper;
using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repositories.UserRepo;
using System.Security.Claims;
using WMSAPI.DTO;
using WMSAPI.TokenRepository;

namespace WMSAPI.Controllers
{
    [Authorize]
    //[Route("api/[controller]")]
    //[ApiController]
    public class UsersController : ODataController
    {
        private readonly IUserRepository repository;
        private readonly ITokenRepository _tokenRepository;
        private IMapper Mapper { get; }

        public UsersController(IMapper mapper, ITokenRepository tokenRepository)
        {
            Mapper = mapper;
            repository = new UserRepository();
            _tokenRepository = tokenRepository;
        }

        [Authorize(Roles = "ADMIN")]
        [EnableQuery]
        public ActionResult<List<UserDTO>> Get()
        {
            return Ok(Mapper.Map<List<UserDTO>>(repository.GetUsers()));
        }
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        [Route("api/[controller]/GetAllUsersByAdmin")]
        public ActionResult<List<UserDTO>> GetAllUsersByAdmin(int? id = 1)
        {
            return Ok(Mapper.Map<List<UserDTO>>(repository.GetUsersByAdminId(id)));
        }
        [Authorize(Roles = "ADMIN, EMPLOYEE")]
        [EnableQuery]
        public ActionResult<UserDTO> Get([FromODataUri] int key)
        {
            var user = Mapper.Map<UserDTO>(repository.GetUserById(key));

            if (user == null)
            {
                return new NotFoundResult();
            }

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/[controller]/Authenticate")]
        public IActionResult Authenticate([FromBody] UserLoginDTO user)
        {
            var token = _tokenRepository.Authenticate(Mapper.Map<User>(user));

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }
        [HttpGet]
        [Route("api/[controller]/Role")]
        public ActionResult GetUserRoleByToken()
        {
            // Get the user's Claims from the authenticated User
            var user = HttpContext.User;

            // Retrieve the 'role' claim from the user's Claims
            var roleClaim = user.FindFirst(c => c.Type.Equals(ClaimTypes.Role));

            if (roleClaim != null)
            {
                var userRole = roleClaim.Value;
                return Ok(new { Role = userRole });
            }
            else
            {
                // Role claim not found, handle as needed (e.g., return an error response)
                return NotFound("Not found type role in token.");
            }
        }

        [Authorize(Roles = "ADMIN")]
        [EnableQuery]
        public IActionResult Post([FromBody] UserRequestDTO userRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = Mapper.Map<User>(userRequest);
            repository.SaveUser(user);

            return Ok(Mapper.Map<UserRequestDTO>(repository.GetUserByLastId()));
        }
        [Authorize(Roles = "ADMIN, EMPLOYEE")]
        [EnableQuery]
        public IActionResult Put([FromODataUri] int key, [FromBody] UserRequestDTO userRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var _user = repository.GetUserById(key);
            var user = Mapper.Map<User>(userRequest);

            if (_user == null || user.Id != key)
            {
                return new NotFoundResult();
            }

            repository.UpdateUser(user);

            return Ok(Mapper.Map<UserRequestDTO>(repository.GetUserById(key)));
        }
        [Authorize(Roles = "ADMIN")]
        [EnableQuery]
        public IActionResult Delete([FromODataUri] int key)
        {
            var user = repository.GetUserById(key);
            var userResponse = Mapper.Map<UserDTO>(user);

            if (user == null)
            {
                return new NotFoundResult();
            }

            repository.DeleteUser(user);

            return Ok(userResponse);
        }
    }
}
