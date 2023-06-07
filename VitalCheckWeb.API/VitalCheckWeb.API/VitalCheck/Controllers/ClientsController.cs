using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Services;
using VitalCheckWeb.API.VitalCheck.Resources;

namespace VitalCheckWeb.API.VitalCheck.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;
    private readonly IMapper _mapper;
    
    public ClientsController(IClientService clientService, IMapper mapper)
    {
        _clientService = clientService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<ClientResource>> GetAllAsync()
    {
        var clients = await _clientService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Client>, IEnumerable<ClientResource>>(clients);
        return resources;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] SaveClientResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var client = _mapper.Map<SaveClientResource, Client>(resource);
        var result = await _clientService.SaveAsync(client);

        if (!result.Success)
            return BadRequest(result.Message);

        var clientResource = _mapper.Map<Client, ClientResource>(result.Resource);
        return Ok(clientResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] SaveClientResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var client = _mapper.Map<SaveClientResource, Client>(resource);
        var result = await _clientService.UpdateAsync(id, client);

        if (!result.Success)
            return BadRequest(result.Message);

        var clientResource = _mapper.Map<Client, ClientResource>(result.Resource);
        return Ok(clientResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _clientService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var clientResource = _mapper.Map<Client, ClientResource>(result.Resource);
        return Ok(clientResource);
    }
}