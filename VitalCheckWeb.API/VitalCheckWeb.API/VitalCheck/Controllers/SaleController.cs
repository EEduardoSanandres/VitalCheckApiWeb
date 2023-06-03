using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Services;
using VitalCheckWeb.API.VitalCheck.Resources;

namespace VitalCheckWeb.API.VitalCheck.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
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
    public async Task<IEnumerable<SaleResource>> GetAllAsync()
    {
        var sales = await _saleService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Sale>, IEnumerable<SaleResource>>(sales);
        return resources;
    }

    [HttpGet("company/{userId}")]
    public async Task<IEnumerable<SaleResource>> GetByCompanyIdAsync(int userId)
    {
        var sales = await _saleService.ListByUserIdAsync(userId);
        var resources = _mapper.Map<IEnumerable<Sale>, IEnumerable<SaleResource>>(sales);
        return resources;
    }

    [HttpGet("client/{clientId}")]
    public async Task<IEnumerable<SaleResource>> GetByClientIdAsync(int clientId)
    {
        var sales = await _saleService.ListByClientIdAsync(clientId);
        var resources = _mapper.Map<IEnumerable<Sale>, IEnumerable<SaleResource>>(sales);
        return resources;
    }

    [HttpGet("medicine/{medicineId}")]
    public async Task<IEnumerable<SaleResource>> GetByMedicineIdAsync(int medicineId)
    {
        var sales = await _saleService.ListByMedicineIdAsync(medicineId);
        var resources = _mapper.Map<IEnumerable<Sale>, IEnumerable<SaleResource>>(sales);
        return resources;
    }

    [HttpPost]
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
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _saleService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var saleResource = _mapper.Map<Sale, SaleResource>(result.Resource);
        return Ok(saleResource);
    }
}