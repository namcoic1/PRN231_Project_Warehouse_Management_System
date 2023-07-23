using AutoMapper;
using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repositories.CarrierRepo;
using WMSAPI.DTO;

namespace WMSAPI.Controllers
{
    [Authorize(Roles = "ADMIN")]
    //[Route("api/[controller]")]
    //[ApiController]
    public class CarriersController : ODataController
    {
        private readonly ICarrierRepository repository;
        private IMapper Mapper { get; }

        public CarriersController(IMapper mapper)
        {
            Mapper = mapper;
            repository = new CarrierRepository();
        }

        [EnableQuery]
        public ActionResult<List<CarrierDTO>> Get()
        {
            return Ok(Mapper.Map<List<CarrierDTO>>(repository.GetCarriers()));
        }
        [EnableQuery]
        public ActionResult<CarrierDTO> Get([FromODataUri] int key)
        {
            var supplier = Mapper.Map<CarrierDTO>(repository.GetCarrierById(key));

            if (supplier == null)
            {
                return new NotFoundResult();
            }

            return Ok(supplier);
        }
        [EnableQuery]
        public IActionResult Post([FromBody] CarrierDTO carrierRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var carrier = Mapper.Map<Carrier>(carrierRequest);
            repository.SaveCarrier(carrier);

            //return Ok(carrierRequest);
            return Ok(Mapper.Map<CarrierDTO>(repository.GetCarrierByLastId()));
        }
        [EnableQuery]
        public IActionResult Put([FromODataUri] int key, [FromBody] CarrierDTO carrierRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var _carrier = repository.GetCarrierById(key);
            var carrier = Mapper.Map<Carrier>(carrierRequest);

            if (_carrier == null || carrier.Id != key)
            {
                return new NotFoundResult();
            }

            repository.UpdateCarrier(carrier);

            //return Ok(carrierRequest);
            return Ok(Mapper.Map<CarrierDTO>(repository.GetCarrierById(key)));
        }
        [EnableQuery]
        public IActionResult Delete([FromODataUri] int key)
        {
            var carrier = repository.GetCarrierById(key);
            var carrierResponse = Mapper.Map<CarrierDTO>(carrier);

            if (carrier == null)
            {
                return new NotFoundResult();
            }

            repository.DeleteCarrier(carrier);

            return Ok(carrierResponse);
        }
    }
}
