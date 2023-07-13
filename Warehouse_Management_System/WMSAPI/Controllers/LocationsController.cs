using AutoMapper;
using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repositories.LocationRepo;
using WMSAPI.DTO;

namespace WMSAPI.Controllers
{
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

        [EnableQuery]
        public ActionResult<List<LocationDTO>> Get()
        {
            return Ok(Mapper.Map<List<LocationDTO>>(repository.GetLocations()));
        }
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
        [EnableQuery]
        public IActionResult Delete([FromODataUri] int key)
        {
            var location = repository.GetLocationById(key);
            var locationResponse = Mapper.Map<LocationDTO>(location);

            if (location == null)
            {
                return new NotFoundResult();
            }

            repository.DeleteLocation(location);

            return Ok(locationResponse);
        }
    }
}
