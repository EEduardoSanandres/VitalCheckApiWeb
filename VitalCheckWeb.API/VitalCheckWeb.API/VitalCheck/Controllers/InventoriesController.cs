using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Services;
using VitalCheckWeb.API.VitalCheck.Resources;

namespace VitalCheckWeb.API.VitalCheck.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Create, read, update and delete Inventory")]
public class InventoriesController : ControllerBase
{
        private readonly IInventoryService _inventoryService;
        private readonly IMapper _mapper;

        public InventoriesController(IInventoryService inventoryService, IMapper mapper)
        {
            _inventoryService = inventoryService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<InventoryResource>), 200)]
        [ProducesResponseType(500)]
        [SwaggerOperation(
            Summary = "Get All Inventories",
            Description = "Get a list of all Inventories",
            OperationId = "GetAllInventories",
            Tags = new[] { "Inventories" }
        )]
        public async Task<IEnumerable<InventoryResource>> GetAllAsync()
        {
            var inventories = await _inventoryService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Inventory>, IEnumerable<InventoryResource>>(inventories);
            return resources;
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(IEnumerable<InventoryResource>), 200)]
        [ProducesResponseType(500)]
        [SwaggerOperation(
            Summary = "Get Inventories by User ID",
            Description = "Get a list of Inventories where User's ID matches the provided ID",
            OperationId = "GetInventoriesByUserId",
            Tags = new[] { "Inventories" }
        )]
        public async Task<IEnumerable<InventoryResource>> GetByUserIdAsync(int userId)
        {
            var inventories = await _inventoryService.ListByUserIdAsync(userId);
            var resources = _mapper.Map<IEnumerable<Inventory>, IEnumerable<InventoryResource>>(inventories);
            return resources;
        }

        [HttpGet("medicine/{medicineId}")]
        [ProducesResponseType(typeof(IEnumerable<InventoryResource>), 200)]
        [ProducesResponseType(500)]
        [SwaggerOperation(
            Summary = "Get Inventories by Medicine ID",
            Description = "Get a list of Inventories where the Medicine's ID matches the provided ID",
            OperationId = "GetInventoriesByMedicineId",
            Tags = new[] { "Inventories" }
        )]
        public async Task<IEnumerable<InventoryResource>> GetByMedicineIdAsync(int medicineId)
        {
            var inventories = await _inventoryService.ListByMedicineIdAsync(medicineId);
            var resources = _mapper.Map<IEnumerable<Inventory>, IEnumerable<InventoryResource>>(inventories);
            return resources;
        }

        [HttpPost]
        [ProducesResponseType(typeof(InventoryResource), 201)]
        [ProducesResponseType(typeof(List<string>), 400)]
        [ProducesResponseType(500)]
        [SwaggerOperation(
            Summary = "Create a new Inventory",
            Description = "Create a new Inventory with the specified information",
            OperationId = "CreateInventory",
            Tags = new[] { "Inventories" }
        )]
        public async Task<IActionResult> CreateAsync([FromBody] SaveInventoryResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var inventory = _mapper.Map<SaveInventoryResource, Inventory>(resource);
            var result = await _inventoryService.SaveAsync(inventory);

            if (!result.Success)
                return BadRequest(result.Message);

            var inventoryResource = _mapper.Map<Inventory, InventoryResource>(result.Resource);
            return Ok(inventoryResource);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(InventoryResource), 201)]
        [ProducesResponseType(typeof(List<string>), 400)]
        [ProducesResponseType(500)]
        [SwaggerOperation(
            Summary = "Update an existing Inventory",
            Description = "Update an existing Inventory with the specified ID and information",
            OperationId = "UpdateInventory",
            Tags = new[] { "Inventories" }
        )]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] SaveInventoryResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var inventory = _mapper.Map<SaveInventoryResource, Inventory>(resource);
            var result = await _inventoryService.UpdateAsync(id, inventory);

            if (!result.Success)
                return BadRequest(result.Message);

            var inventoryResource = _mapper.Map<Inventory, InventoryResource>(result.Resource);
            return Ok(inventoryResource);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(InventoryResource), 201)]
        [ProducesResponseType(typeof(List<string>), 400)]
        [ProducesResponseType(500)]
        [SwaggerOperation(
            Summary = "Delete an existing Inventory",
            Description = "Delete an existing Inventory with the specified ID",
            OperationId = "DeleteInventory",
            Tags = new[] { "Inventories" }
        )]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _inventoryService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var inventoryResource = _mapper.Map<Inventory, InventoryResource>(result.Resource);
            return Ok(inventoryResource);
        }
}