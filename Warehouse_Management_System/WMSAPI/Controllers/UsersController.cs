using AutoMapper;
using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repositories.UserRepo;
using WMSAPI.DTO;

namespace WMSAPI.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class UsersController : ODataController
    {
        private readonly IUserRepository repository;
        private IMapper Mapper { get; }

        public UsersController(IMapper mapper)
        {
            Mapper = mapper;
            repository = new UserRepository();
        }

        [EnableQuery]
        public ActionResult<List<UserDTO>> Get()
        {
            return Ok(Mapper.Map<List<UserDTO>>(repository.GetUsers()));
        }
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
