using AutoMapper;
using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repositories.SupplierRepo;
using WMSAPI.DTO;

namespace WMSAPI.Controllers
{
    [Authorize(Roles = "ADMIN")]
    //[Route("api/[controller]")]
    //[ApiController]
    public class SuppliersController : ODataController
    {
        private readonly ISupplierRepository repository;
        private IMapper Mapper { get; }

        public SuppliersController(IMapper mapper)
        {
            Mapper = mapper;
            repository = new SupplierRepository();
        }

        [EnableQuery]
        public ActionResult<List<SupplierDTO>> Get()
        {
            return Ok(Mapper.Map<List<SupplierDTO>>(repository.GetSuppliers()));
        }
        [EnableQuery]
        public ActionResult<SupplierDTO> Get([FromODataUri] string key)
        {
            var supplier = Mapper.Map<SupplierDTO>(repository.GetSupplierById(key));

            if (supplier == null)
            {
                return new NotFoundResult();
            }

            return Ok(supplier);
        }
        [EnableQuery]
        public IActionResult Post([FromBody] SupplierDTO supplierRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var supplier = Mapper.Map<Supplier>(supplierRequest);
            repository.SaveSupplier(supplier);

            return Ok(Mapper.Map<SupplierDTO>(repository.GetSupplierByLastId()));
        }
        [EnableQuery]
        public IActionResult Put([FromODataUri] string key, [FromBody] SupplierDTO supplierRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var _supplier = repository.GetSupplierById(key);
            var supplier = Mapper.Map<Supplier>(supplierRequest);

            if (_supplier == null || !supplier.Id.Equals(key))
            {
                return new NotFoundResult();
            }

            repository.UpdateSupplier(supplier);

            return Ok(Mapper.Map<SupplierDTO>(repository.GetSupplierById(key)));
        }
        [EnableQuery]
        public IActionResult Delete([FromODataUri] string key)
        {
            var supplier = repository.GetSupplierById(key);
            var supplierResponse = Mapper.Map<SupplierDTO>(supplier);

            if (supplier == null)
            {
                return new NotFoundResult();
            }

            repository.DeleteSupplier(supplier);

            return Ok(supplierResponse);
        }
    }
}
