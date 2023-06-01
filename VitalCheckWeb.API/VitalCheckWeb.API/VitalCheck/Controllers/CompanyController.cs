using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Services;
using VitalCheckWeb.API.VitalCheck.Resources;

namespace VitalCheckWeb.API.VitalCheck.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class CompaniesController : ControllerBase
{
    private readonly ICompanyService _companyService;
    private readonly IMapper _mapper;

    public CompaniesController(ICompanyService companyService, IMapper mapper)
    {
        _companyService = companyService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<CompanyResource>> GetAllAsync()
    {
        var companies = await _companyService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Company>, IEnumerable<CompanyResource>>(companies);
        return resources;
    }

    [HttpGet("{userId}")]
    public async Task<IEnumerable<CompanyResource>> GetByUserIdAsync(int userId)
    {
        var companies = await _companyService.ListByUserIdAsync(userId);
        var resources = _mapper.Map<IEnumerable<Company>, IEnumerable<CompanyResource>>(companies);
        return resources;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] SaveCompanyResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var company = _mapper.Map<SaveCompanyResource, Company>(resource);
        var result = await _companyService.SaveAsync(company);

        if (!result.Success)
            return BadRequest(result.Message);

        var companyResource = _mapper.Map<Company, CompanyResource>(result.Resource);
        return Ok(companyResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] SaveCompanyResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var company = _mapper.Map<SaveCompanyResource, Company>(resource);
        var result = await _companyService.UpdateAsync(id, company);

        if (!result.Success)
            return BadRequest(result.Message);

        var companyResource = _mapper.Map<Company, CompanyResource>(result.Resource);
        return Ok(companyResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _companyService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var companyResource = _mapper.Map<Company, CompanyResource>(result.Resource);
        return Ok(companyResource);
    }
}