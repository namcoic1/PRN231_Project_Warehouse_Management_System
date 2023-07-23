using AutoMapper;
using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repositories.LocationRepo;
using WMSAPI.DTO;

namespace WMSAPI.Controllers
{
    [Authorize]
    //[Route("api/[controller]")]
    //[ApiController]
    public class LocationsController : ODataController
    {
        private readonly ILocationRepository repository;
        private IMapper Mapper { get; }

        public LocationsController(IMapper mapper)
        {
            Mapper = mapper;
            repository = new LocationRepository();
        }

        [Authorize(Roles = "ADMIN")]
        [EnableQuery]
        public ActionResult<List<LocationDTO>> Get()
        {
            return Ok(Mapper.Map<List<LocationDTO>>(repository.GetLocations()));
        }
        [Authorize(Roles = "EMPLOYEE")]
        [HttpGet]
        [Route("api/[controller]/GetLocationByUser")]
        public ActionResult<List<LocationDTO>> GetLocationByUser(int? id = 2)
        {
            return Ok(Mapper.Map<List<LocationDTO>>(repository.GetLocationByUserId(id)));
        }
        [Authorize(Roles = "ADMIN, EMPLOYEE")]
        [EnableQuery]
        public ActionResult<LocationDTO> Get([FromODataUri] int key)
        {
            var location = Mapper.Map<LocationDTO>(repository.GetLocationById(key));

            if (location == null)
            {
                return new NotFoundResult();
            }

            return Ok(location);
        }
        [Authorize(Roles = "ADMIN")]
        [EnableQuery]
        public IActionResult Post([FromBody] LocationRequestDTO locationRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var location = Mapper.Map<Location>(locationRequest);
            repository.SaveLocation(location);

            return Ok(Mapper.Map<LocationRequestDTO>(repository.GetLocationByLastId()));
        }
        [Authorize(Roles = "ADMIN, EMPLOYEE")]
        [EnableQuery]
        public IActionResult Put([FromODataUri] int key, [FromBody] LocationRequestDTO locationRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var _location = repository.GetLocationById(key);
            var location = Mapper.Map<Location>(locationRequest);

            if (_location == null || location.Id != key)
            {
                return new NotFoundResult();
            }

            repository.UpdateLocation(location);

            return Ok(Mapper.Map<LocationRequestDTO>(repository.GetLocationById(key)));
        }
        // do not delete location
        //[EnableQuery]
        //public IActionResult Delete([FromODataUri] int key)
        //{
        //    var location = repository.GetLocationById(key);
        //    var locationResponse = Mapper.Map<LocationDTO>(location);

        //    if (location == null)
        //    {
        //        return new NotFoundResult();
        //    }

        //    repository.DeleteLocation(location);

        //    return Ok(locationResponse);
        //}
    }
}
