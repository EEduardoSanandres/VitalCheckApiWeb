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
[SwaggerTag("Create, read, update and delete Clients")]
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
    [ProducesResponseType(typeof(IEnumerable<ClientResource>), 200)]
    [ProducesResponseType(500)]
    [SwaggerOperation(
        Summary = "Get All Clients",
        Description = "Get a list of all existing clients",
        OperationId = "GetAllClients",
        Tags = new[] { "Clients" }
    )]
    public async Task<IEnumerable<ClientResource>> GetAllAsync()
    {
        var clients = await _clientService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Client>, IEnumerable<ClientResource>>(clients);
        return resources;
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(ClientResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    [SwaggerOperation(
        Summary = "Create a new Client",
        Description = "Create a new Client with the specified information",
        OperationId = "CreateClient",
        Tags = new[] { "Clients" }
    )]
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
    [ProducesResponseType(typeof(ClientResource), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    [SwaggerOperation(
        Summary = "Update an existing Client",
        Description = "Update an existing Client with the specified information",
        OperationId = "UpdateClient",
        Tags = new[] { "Clients" }
    )]
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
    [ProducesResponseType(typeof(ClientResource), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    [SwaggerOperation(
        Summary = "Delete an existing Client",
        Description = "Delete an existing Client with the specified ID",
        OperationId = "DeleteClient",
        Tags = new[] { "Clients" }
    )]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _clientService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var clientResource = _mapper.Map<Client, ClientResource>(result.Resource);
        return Ok(clientResource);
    }
}