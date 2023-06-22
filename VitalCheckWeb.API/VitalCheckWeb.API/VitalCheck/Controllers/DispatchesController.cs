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
[SwaggerTag("Create, read, update and delete Dispatches")]
public class DispatchesController : ControllerBase
{
        private readonly IDispatchService _dispatchService;
        private readonly IMapper _mapper;

        public DispatchesController(IDispatchService dispatchService, IMapper mapper)
        {
            _dispatchService = dispatchService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DispatchResource>), 200)]
        [ProducesResponseType(500)]
        [SwaggerOperation(
            Summary = "Get All Dispatches",
            Description = "Get a list of all existing dispatches",
            OperationId = "GetAllDispatches",
            Tags = new[] { "Dispatches" }
        )]
        public async Task<IEnumerable<DispatchResource>> GetAllAsync()
        {
            var dispatches = await _dispatchService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Dispatch>, IEnumerable<DispatchResource>>(dispatches);
            return resources;
        }

        [HttpGet("user1/{user1Id}")]
        [ProducesResponseType(typeof(IEnumerable<DispatchResource>), 200)]
        [ProducesResponseType(500)]
        [SwaggerOperation(
            Summary = "Get Dispatches by User1 ID",
            Description = "Get a list of Dispatches where User1's ID matches the provided ID",
            OperationId = "GetDispatchesByUser1Id",
            Tags = new[] { "Dispatches" }
        )]
        public async Task<IEnumerable<DispatchResource>> GetByUser1IdAsync(int user1Id)
        {
            var dispatches = await _dispatchService.ListByUser1IdAsync(user1Id);
            var resources = _mapper.Map<IEnumerable<Dispatch>, IEnumerable<DispatchResource>>(dispatches);
            return resources;
        }

        [HttpGet("user2/{user2Id}")]
        [ProducesResponseType(typeof(IEnumerable<DispatchResource>), 200)]
        [ProducesResponseType(500)]
        [SwaggerOperation(
            Summary = "Get Dispatches by User2 ID",
            Description = "Get a list of Dispatches where User2's ID matches the provided ID",
            OperationId = "GetDispatchesByUser2Id",
            Tags = new[] { "Dispatches" }
        )]
        public async Task<IEnumerable<DispatchResource>> GetByUser2IdAsync(int user2Id)
        {
            var dispatches = await _dispatchService.ListByUser2IdAsync(user2Id);
            var resources = _mapper.Map<IEnumerable<Dispatch>, IEnumerable<DispatchResource>>(dispatches);
            return resources;
        }

        [HttpGet("medicine/{medicineId}")]
        [ProducesResponseType(typeof(IEnumerable<DispatchResource>), 200)]
        [ProducesResponseType(500)]
        [SwaggerOperation(
            Summary = "Get Dispatches by Medicine ID",
            Description = "Get a list of Dispatches where the Medicine's ID matches the provided ID",
            OperationId = "GetDispatchesByMedicineId",
            Tags = new[] { "Dispatches" }
        )]
        public async Task<IEnumerable<DispatchResource>> GetByMedicineIdAsync(int medicineId)
        {
            var dispatches = await _dispatchService.ListByMedicineIdAsync(medicineId);
            var resources = _mapper.Map<IEnumerable<Dispatch>, IEnumerable<DispatchResource>>(dispatches);
            return resources;
        }

        [HttpPost]
        [ProducesResponseType(typeof(DispatchResource), 201)]
        [ProducesResponseType(typeof(List<string>), 400)]
        [ProducesResponseType(500)]
        [SwaggerOperation(
            Summary = "Create a new Dispatch",
            Description = "Create a new Dispatch with the specified information",
            OperationId = "CreateDispatch",
            Tags = new[] { "Dispatches" }
        )]
        public async Task<IActionResult> CreateAsync([FromBody] SaveDispatchResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dispatch = _mapper.Map<SaveDispatchResource, Dispatch>(resource);
            var result = await _dispatchService.SaveAsync(dispatch);

            if (!result.Success)
                return BadRequest(result.Message);

            var dispatchResource = _mapper.Map<Dispatch, DispatchResource>(result.Resource);
            return Ok(dispatchResource);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(DispatchResource), 201)]
        [ProducesResponseType(typeof(List<string>), 400)]
        [ProducesResponseType(500)]
        [SwaggerOperation(
            Summary = "Update an existing Dispatch",
            Description = "Update an existing Dispatch with the specified ID and information",
            OperationId = "UpdateDispatch",
            Tags = new[] { "Dispatches" }
        )]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] SaveDispatchResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dispatch = _mapper.Map<SaveDispatchResource, Dispatch>(resource);
            var result = await _dispatchService.UpdateAsync(id, dispatch);

            if (!result.Success)
                return BadRequest(result.Message);

            var dispatchResource = _mapper.Map<Dispatch, DispatchResource>(result.Resource);
            return Ok(dispatchResource);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DispatchResource), 201)]
        [ProducesResponseType(typeof(List<string>), 400)]
        [ProducesResponseType(500)]
        [SwaggerOperation(
            Summary = "Delete an existing Dispatch",
            Description = "Delete an existing Dispatch with the specified ID",
            OperationId = "DeleteDispatch",
            Tags = new[] { "Dispatches" }
        )]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _dispatchService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var dispatchResource = _mapper.Map<Dispatch, DispatchResource>(result.Resource);
            return Ok(dispatchResource); 
        }
}