using AutoMapper;
using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repositories.RoleRepo;
using WarehouseMSAPI.DTO;

namespace WMSAPI.Controllers
{
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
        public ActionResult<List<RoleResponseDTO>> Get()
        {
            return Ok(Mapper.Map<List<RoleResponseDTO>>(repository.GetRoles()));
        }
        [EnableQuery]
        //public ActionResult<RoleDTO> Get(int key)
        public ActionResult<RoleResponseDTO> Get([FromODataUri] int key)
        {
            var role = Mapper.Map<RoleResponseDTO>(repository.GetRoleById(key));

            if (role == null)
            {
                return new NotFoundResult();
            }

            return Ok(role);
        }
        [EnableQuery]
        public IActionResult Post([FromBody] RoleRequestDTO roleRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = Mapper.Map<Role>(roleRequest);
            repository.SaveRole(role);

            //return NoContent();
            return Ok(roleRequest);
        }
        [EnableQuery]
        public IActionResult Put([FromODataUri] int key, [FromBody] RoleRequestDTO roleRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var _role = repository.GetRoleById(key);
            var role = Mapper.Map<Role>(roleRequest);

            if (_role == null || role.ID != key)
            {
                return new NotFoundResult();
            }

            repository.UpdateRole(role);

            return Ok(role);
        }
    }
}
