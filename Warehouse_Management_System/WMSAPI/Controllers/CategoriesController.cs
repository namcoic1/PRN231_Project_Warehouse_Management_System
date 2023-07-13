using AutoMapper;
using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repositories.CategoryRepo;
using WMSAPI.DTO;

namespace WMSAPI.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class CategoriesController : ODataController
    {
        private readonly ICategoryRepository repository;
        private IMapper Mapper { get; }

        public CategoriesController(IMapper mapper)
        {
            Mapper = mapper;
            repository = new CategoryRepository();
        }

        [EnableQuery]
        public ActionResult<List<CategoryDTO>> Get()
        {
            return Ok(Mapper.Map<List<CategoryDTO>>(repository.GetCategories()));
        }
        [EnableQuery]
        public ActionResult<CategoryDTO> Get([FromODataUri] int key)
        {
            var category = Mapper.Map<CategoryDTO>(repository.GetCategoryById(key));

            if (category == null)
            {
                return new NotFoundResult();
            }

            return Ok(category);
        }
        [EnableQuery]
        public IActionResult Post([FromBody] CategoryDTO categoryRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = Mapper.Map<Category>(categoryRequest);
            repository.SaveCategory(category);

            return Ok(Mapper.Map<CategoryDTO>(repository.GetCategoryByLastId()));
        }
        [EnableQuery]
        public IActionResult Put([FromODataUri] int key, [FromBody] CategoryDTO categoryRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var _category = repository.GetCategoryById(key);
            var category = Mapper.Map<Category>(categoryRequest);

            if (_category == null || category.Id != key)
            {
                return new NotFoundResult();
            }

            repository.UpdateCategory(category);

            return Ok(Mapper.Map<CategoryDTO>(repository.GetCategoryById(key)));
        }
        [EnableQuery]
        public IActionResult Delete([FromODataUri] int key)
        {
            var category = repository.GetCategoryById(key);
            var categoryResponse = Mapper.Map<CategoryDTO>(category);

            if (category == null)
            {
                return new NotFoundResult();
            }

            repository.DeleteCategory(category);

            return Ok(categoryResponse);
        }
    }
}
