using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Services;
using VitalCheckWeb.API.VitalCheck.Resources;

namespace VitalCheckWeb.API.VitalCheck.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class ProvidersController : ControllerBase
{
    private readonly IProviderService _providerService;
    private readonly IMapper _mapper;

    public ProvidersController(IProviderService providerService, IMapper mapper)
    {
        _providerService = providerService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<ProviderResource>> GetAllAsync()
    {
        var providers = await _providerService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Provider>, IEnumerable<ProviderResource>>(providers);
        return resources;
    }

    [HttpGet("{userId}")]
    public async Task<IEnumerable<ProviderResource>> GetByUserIdAsync(int userId)
    {
        var providers = await _providerService.ListByUserIdAsync(userId);
        var resources = _mapper.Map<IEnumerable<Provider>, IEnumerable<ProviderResource>>(providers);
        return resources;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] SaveProviderResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var provider = _mapper.Map<SaveProviderResource, Provider>(resource);
        var result = await _providerService.SaveAsync(provider);

        if (!result.Success)
            return BadRequest(result.Message);

        var providerResource = _mapper.Map<Provider, ProviderResource>(result.Resource);
        return Ok(providerResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] SaveProviderResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var provider = _mapper.Map<SaveProviderResource, Provider>(resource);
        var result = await _providerService.UpdateAsync(id, provider);

        if (!result.Success)
            return BadRequest(result.Message);

        var providerResource = _mapper.Map<Provider, ProviderResource>(result.Resource);
        return Ok(providerResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _providerService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var providerResource = _mapper.Map<Provider, ProviderResource>(result.Resource);
        return Ok(providerResource);
    }
}