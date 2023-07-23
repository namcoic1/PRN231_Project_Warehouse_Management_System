using AutoMapper;
using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repositories.InventoryRepo;
using WMSAPI.DTO;

namespace WMSAPI.Controllers
{
    [Authorize(Roles = "ADMIN")]
    //[Route("api/[controller]")]
    //[ApiController]
    public class InventoriesController : ODataController
    {
        private readonly IInventoryRepository repository;
        private IMapper Mapper { get; }

        public InventoriesController(IMapper mapper)
        {
            Mapper = mapper;
            repository = new InventoryRepository();
        }

        [EnableQuery]
        public ActionResult<List<InventoryDTO>> Get()
        {
            return Ok(Mapper.Map<List<InventoryDTO>>(repository.GetInventories()));
        }
        [EnableQuery]
        public ActionResult<InventoryDTO> Get([FromODataUri] string key)
        {
            var inventory = Mapper.Map<InventoryDTO>(repository.GetInventoryById(key));

            if (inventory == null)
            {
                return new NotFoundResult();
            }

            return Ok(inventory);
        }
        [EnableQuery]
        public IActionResult Post([FromBody] InventoryRequestDTO inventoryRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var inventory = Mapper.Map<Inventory>(inventoryRequest);
            repository.SaveInventory(inventory);

            return Ok(Mapper.Map<InventoryRequestDTO>(repository.GetInventoryByLastId()));
        }
        [EnableQuery]
        public IActionResult Put([FromODataUri] string key, [FromBody] InventoryRequestDTO inventoryRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var _inventory = repository.GetInventoryById(key);
            var inventory = Mapper.Map<Inventory>(inventoryRequest);

            if (_inventory == null || !inventory.Id.Equals(key))
            {
                return new NotFoundResult();
            }

            repository.UpdateInventory(inventory);

            return Ok(Mapper.Map<InventoryRequestDTO>(repository.GetInventoryById(key)));
        }
        [EnableQuery]
        public IActionResult Delete([FromODataUri] string key)
        {
            var inventory = repository.GetInventoryById(key);
            var inventoryResponse = Mapper.Map<InventoryDTO>(inventory);

            if (inventory == null)
            {
                return new NotFoundResult();
            }

            repository.DeleteInventory(inventory);

            return Ok(inventoryResponse);
        }
    }
}
