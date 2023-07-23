using AutoMapper;
using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repositories.CustomerRepo;
using WMSAPI.DTO;

namespace WMSAPI.Controllers
{
    [Authorize(Roles = "ADMIN")]
    //[Route("api/[controller]")]
    //[ApiController]
    public class CustomersController : ODataController
    {
        private readonly ICustomerRepository repository;
        private IMapper Mapper { get; }

        public CustomersController(IMapper mapper)
        {
            Mapper = mapper;
            repository = new CustomerRepository();
        }

        [EnableQuery]
        public ActionResult<List<CustomerDTO>> Get()
        {
            return Ok(Mapper.Map<List<CustomerDTO>>(repository.GetCustomers()));
        }
        [EnableQuery]
        public ActionResult<CustomerDTO> Get([FromODataUri] string key)
        {
            var customer = Mapper.Map<CustomerDTO>(repository.GetCustomerById(key));

            if (customer == null)
            {
                return new NotFoundResult();
            }

            return Ok(customer);
        }
        [EnableQuery]
        public IActionResult Post([FromBody] CustomerDTO customerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = Mapper.Map<Customer>(customerRequest);
            repository.SaveCustomer(customer);

            return Ok(Mapper.Map<CustomerDTO>(repository.GetCustomerByLastId()));
        }
        [EnableQuery]
        public IActionResult Put([FromODataUri] string key, [FromBody] CustomerDTO customerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var _customer = repository.GetCustomerById(key);
            var customer = Mapper.Map<Customer>(customerRequest);

            if (_customer == null || !customer.Id.Equals(key))
            {
                return new NotFoundResult();
            }

            repository.UpdateCustomer(customer);

            return Ok(Mapper.Map<CustomerDTO>(repository.GetCustomerById(key)));
        }
        [EnableQuery]
        public IActionResult Delete([FromODataUri] string key)
        {
            var customer = repository.GetCustomerById(key);
            var customerResponse = Mapper.Map<CustomerDTO>(customer);

            if (customer == null)
            {
                return new NotFoundResult();
            }

            repository.DeleteCustomer(customer);

            return Ok(customerResponse);
        }
    }
}
