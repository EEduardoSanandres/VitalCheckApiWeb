using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Services;
using VitalCheckWeb.API.VitalCheck.Resources;

namespace VitalCheckWeb.API.VitalCheck.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class DispatchController : ControllerBase
{
        private readonly IDispatchService _dispatchService;
        private readonly IMapper _mapper;

        public DispatchController(IDispatchService dispatchService, IMapper mapper)
        {
            _dispatchService = dispatchService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<DispatchResource>> GetAllAsync()
        {
            var dispatches = await _dispatchService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Dispatch>, IEnumerable<DispatchResource>>(dispatches);
            return resources;
        }

        [HttpGet("company/{companyId}")]
        public async Task<IEnumerable<DispatchResource>> GetByCompanyIdAsync(int companyId)
        {
            var dispatches = await _dispatchService.ListByCompanyIdAsync(companyId);
            var resources = _mapper.Map<IEnumerable<Dispatch>, IEnumerable<DispatchResource>>(dispatches);
            return resources;
        }

        [HttpGet("provider/{providerId}")]
        public async Task<IEnumerable<DispatchResource>> GetByProviderIdAsync(int providerId)
        {
            var dispatches = await _dispatchService.ListByProviderIdAsync(providerId);
            var resources = _mapper.Map<IEnumerable<Dispatch>, IEnumerable<DispatchResource>>(dispatches);
            return resources;
        }

        [HttpGet("medicine/{medicineId}")]
        public async Task<IEnumerable<DispatchResource>> GetByMedicineIdAsync(int medicineId)
        {
            var dispatches = await _dispatchService.ListByMedicineIdAsync(medicineId);
            var resources = _mapper.Map<IEnumerable<Dispatch>, IEnumerable<DispatchResource>>(dispatches);
            return resources;
        }

        [HttpPost]
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
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _dispatchService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var dispatchResource = _mapper.Map<Dispatch, DispatchResource>(result.Resource);
            return Ok(dispatchResource); 
        }
}