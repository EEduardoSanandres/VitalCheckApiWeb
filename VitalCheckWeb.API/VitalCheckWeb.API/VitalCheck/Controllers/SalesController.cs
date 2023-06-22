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
[SwaggerTag("Create, read, update and delete Sales")]
public class SalesController : ControllerBase
{
    private readonly ISaleService _saleService;
    private readonly IMapper _mapper;

    public SalesController(ISaleService saleService, IMapper mapper)
    {
        _saleService = saleService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SaleResource>), 200)]
    [ProducesResponseType(500)]
    [SwaggerOperation(
        Summary = "Get All Sales",
        Description = "Get a list of all Sales",
        OperationId = "GetAllSales",
        Tags = new[] { "Sales" }
    )]
    public async Task<IEnumerable<SaleResource>> GetAllAsync()
    {
        var sales = await _saleService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Sale>, IEnumerable<SaleResource>>(sales);
        return resources;
    }

    [HttpGet("company/{userId}")]
    [ProducesResponseType(typeof(IEnumerable<SaleResource>), 200)]
    [ProducesResponseType(500)]
    [SwaggerOperation(
        Summary = "Get Sales by Company ID",
        Description = "Get a list of Sales associated with the specified Company ID",
        OperationId = "GetSalesByCompanyId",
        Tags = new[] { "Sales" }
    )]
    public async Task<IEnumerable<SaleResource>> GetByCompanyIdAsync(int userId)
    {
        var sales = await _saleService.ListByUserIdAsync(userId);
        var resources = _mapper.Map<IEnumerable<Sale>, IEnumerable<SaleResource>>(sales);
        return resources;
    }

    [HttpGet("client/{clientId}")]
    [ProducesResponseType(typeof(IEnumerable<SaleResource>), 200)]
    [ProducesResponseType(500)]
    [SwaggerOperation(
        Summary = "Get Sales by Client ID",
        Description = "Get a list of Sales associated with the specified Client ID",
        OperationId = "GetSalesByClientId",
        Tags = new[] { "Sales" }
    )]
    public async Task<IEnumerable<SaleResource>> GetByClientIdAsync(int clientId)
    {
        var sales = await _saleService.ListByClientIdAsync(clientId);
        var resources = _mapper.Map<IEnumerable<Sale>, IEnumerable<SaleResource>>(sales);
        return resources;
    }

    [HttpGet("medicine/{medicineId}")]
    [ProducesResponseType(typeof(IEnumerable<SaleResource>), 200)]
    [ProducesResponseType(500)]
    [SwaggerOperation(
        Summary = "Get Sales by Medicine ID",
        Description = "Get a list of Sales associated with the specified Medicine ID",
        OperationId = "GetSalesByMedicineId",
        Tags = new[] { "Sales" }
    )]
    public async Task<IEnumerable<SaleResource>> GetByMedicineIdAsync(int medicineId)
    {
        var sales = await _saleService.ListByMedicineIdAsync(medicineId);
        var resources = _mapper.Map<IEnumerable<Sale>, IEnumerable<SaleResource>>(sales);
        return resources;
    }

    [HttpPost]
    [ProducesResponseType(typeof(SaleResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    [SwaggerOperation(
        Summary = "Create a new Sale",
        Description = "Create a new Sale with the specified information",
        OperationId = "CreateSale",
        Tags = new[] { "Sales" }
    )]
    public async Task<IActionResult> CreateAsync([FromBody] SaveSaleResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var sale = _mapper.Map<SaveSaleResource, Sale>(resource);
        sale.Date = DateTime.Now; // Set the current date/time

        var result = await _saleService.SaveAsync(sale);

        if (!result.Success)
            return BadRequest(result.Message);

        var saleResource = _mapper.Map<Sale, SaleResource>(result.Resource);
        return Ok(saleResource);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(SaleResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    [SwaggerOperation(
        Summary = "Update an existing Sale",
        Description = "Update an existing Sale with the specified ID and information",
        OperationId = "UpdateSale",
        Tags = new[] { "Sales" }
    )]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] SaveSaleResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var sale = _mapper.Map<SaveSaleResource, Sale>(resource);
        sale.SaleID = id; // Set the sale ID

        var result = await _saleService.UpdateAsync(id, sale);

        if (!result.Success)
            return BadRequest(result.Message);

        var saleResource = _mapper.Map<Sale, SaleResource>(result.Resource);
        return Ok(saleResource);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(SaleResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    [SwaggerOperation(
        Summary = "Delete an existing Sale",
        Description = "Delete an existing Sale with the specified ID",
        OperationId = "DeleteSale",
        Tags = new[] { "Sales" }
    )]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _saleService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var saleResource = _mapper.Map<Sale, SaleResource>(result.Resource);
        return Ok(saleResource);
    }
}