using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repositories.RoleRepo;
using WarehouseMSAPI.DTO;

namespace WMSAPI.Controllers
{
    // authenticate and authorize
    [Authorize(Roles = "ADMIN")]
    //    [Route("api/[controller]")]
    //    [ApiController]
    //    public class RolesController : ControllerBase
    public class RolesController : ODataController
    {
        private readonly IRoleRepository repository;
        private IMapper Mapper { get; }

        public RolesController(IMapper mapper)
        {
            Mapper = mapper;
            repository = new RoleRepository();
        }

        [EnableQuery]
        public ActionResult<List<RoleDTO>> Get()
        {
            return Ok(Mapper.Map<List<RoleDTO>>(repository.GetRoles()));
        }
        [EnableQuery]
        //public ActionResult<RoleDTO> Get(int key)
        public ActionResult<RoleDTO> Get([FromODataUri] int key)
        {
            var role = Mapper.Map<RoleDTO>(repository.GetRoleById(key));

            if (role == null)
            {
                return new NotFoundResult();
            }

            return Ok(role);
        }

        // do not to cud role
        //[EnableQuery]
        //public IActionResult Post([FromBody] RoleDTO roleRequest)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var role = Mapper.Map<Role>(roleRequest);
        //    repository.SaveRole(role);

        //    //return NoContent();
        //    return Ok(Mapper.Map<RoleDTO>(repository.GetRoleByLastId()));
        //}
        //[EnableQuery]
        //public IActionResult Put([FromODataUri] int key, [FromBody] RoleDTO roleRequest)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var _role = repository.GetRoleById(key);
        //    var role = Mapper.Map<Role>(roleRequest);

        //    if (_role == null || role.Id != key)
        //    {
        //        return new NotFoundResult();
        //    }

        //    repository.UpdateRole(role);

        //    return Ok(Mapper.Map<RoleDTO>(repository.GetRoleById(key)));
        //}
        //[EnableQuery]
        //public IActionResult Delete([FromODataUri] int key)
        //{
        //    var role = repository.GetRoleById(key);
        //    var roleResponse = Mapper.Map<RoleDTO>(role);

        //    if (role == null)
        //    {
        //        return new NotFoundResult();
        //    }

        //    repository.DeleteRole(role);

        //    return Ok(roleResponse);
        //}
    }
}
