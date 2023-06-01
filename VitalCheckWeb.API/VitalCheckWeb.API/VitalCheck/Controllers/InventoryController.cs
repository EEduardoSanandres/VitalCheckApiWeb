using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Services;
using VitalCheckWeb.API.VitalCheck.Resources;

namespace VitalCheckWeb.API.VitalCheck.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class InventoryController : ControllerBase
{
        private readonly IInventoryService _inventoryService;
        private readonly IMapper _mapper;

        public InventoryController(IInventoryService inventoryService, IMapper mapper)
        {
            _inventoryService = inventoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<InventoryResource>> GetAllAsync()
        {
            var inventories = await _inventoryService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Inventory>, IEnumerable<InventoryResource>>(inventories);
            return resources;
        }

        [HttpGet("user/{userId}")]
        public async Task<IEnumerable<InventoryResource>> GetByUserIdAsync(int userId)
        {
            var inventories = await _inventoryService.ListByUserIdAsync(userId);
            var resources = _mapper.Map<IEnumerable<Inventory>, IEnumerable<InventoryResource>>(inventories);
            return resources;
        }

        [HttpGet("medicine/{medicineId}")]
        public async Task<IEnumerable<InventoryResource>> GetByMedicineIdAsync(int medicineId)
        {
            var inventories = await _inventoryService.ListByMedicineIdAsync(medicineId);
            var resources = _mapper.Map<IEnumerable<Inventory>, IEnumerable<InventoryResource>>(inventories);
            return resources;
        }

        [HttpPost]
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
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _inventoryService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var inventoryResource = _mapper.Map<Inventory, InventoryResource>(result.Resource);
            return Ok(inventoryResource);
        }
}