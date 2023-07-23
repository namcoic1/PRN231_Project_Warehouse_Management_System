using AutoMapper;
using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repositories.ProductRepo;
using WMSAPI.DTO;

namespace WMSAPI.Controllers
{
    [Authorize(Roles = "ADMIN")]
    //[Route("api/[controller]")]
    //[ApiController]
    public class ProductsController : ODataController
    {
        private readonly IProductRepository repository;
        private IMapper Mapper { get; }

        public ProductsController(IMapper mapper)
        {
            Mapper = mapper;
            repository = new ProductRepository();
        }

        [EnableQuery]
        public ActionResult<List<ProductDTO>> Get()
        {
            return Ok(Mapper.Map<List<ProductDTO>>(repository.GetProducts()));
        }
        [EnableQuery]
        public ActionResult<ProductDTO> Get([FromODataUri] int key)
        {
            var product = Mapper.Map<ProductDTO>(repository.GetProductById(key));

            if (product == null)
            {
                return new NotFoundResult();
            }

            return Ok(product);
        }
        [EnableQuery]
        public IActionResult Post([FromBody] ProductRequestDTO productRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = Mapper.Map<Product>(productRequest);
            repository.SaveProduct(product);

            return Ok(Mapper.Map<ProductRequestDTO>(repository.GetProductByLastId()));
        }
        [EnableQuery]
        public IActionResult Put([FromODataUri] int key, [FromBody] ProductRequestDTO productRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var _product = repository.GetProductById(key);
            var product = Mapper.Map<Product>(productRequest);

            if (_product == null || product.Id != key)
            {
                return new NotFoundResult();
            }

            repository.UpdateProduct(product);

            return Ok(Mapper.Map<ProductRequestDTO>(repository.GetProductById(key)));
        }
        [EnableQuery]
        public IActionResult Delete([FromODataUri] int key)
        {
            var product = repository.GetProductById(key);
            var productResponse = Mapper.Map<ProductDTO>(product);

            if (product == null)
            {
                return new NotFoundResult();
            }

            repository.DeleteProduct(product);

            return Ok(productResponse);
        }
    }
}
