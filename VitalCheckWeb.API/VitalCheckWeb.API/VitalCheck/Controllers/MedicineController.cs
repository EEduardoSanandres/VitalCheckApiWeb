using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Services;
using VitalCheckWeb.API.VitalCheck.Resources;

namespace VitalCheckWeb.API.VitalCheck.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class MedicinesController : ControllerBase
{
    private readonly IMedicineService _medicineService;
    private readonly IMapper _mapper;

    public MedicinesController(IMedicineService medicineService, IMapper mapper)
    {
        _medicineService = medicineService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<MedicineResource>> GetAllAsync()
    {
        var medicines = await _medicineService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Medicine>, IEnumerable<MedicineResource>>(medicines);
        return resources;
    }

    [HttpGet("medicineType/{medicineTypeId}")]
    public async Task<IEnumerable<MedicineResource>> GetByMedicineTypeIdAsync(int medicineTypeId)
    {
        var medicines = await _medicineService.ListByMedicineTypeIdAsync(medicineTypeId);
        var resources = _mapper.Map<IEnumerable<Medicine>, IEnumerable<MedicineResource>>(medicines);
        return resources;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] SaveMedicineResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var medicine = _mapper.Map<SaveMedicineResource, Medicine>(resource);
        var result = await _medicineService.SaveAsync(medicine);

        if (!result.Success)
            return BadRequest(result.Message);

        var medicineResource = _mapper.Map<Medicine, MedicineResource>(result.Resource);
        return Ok(medicineResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] SaveMedicineResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var medicine = _mapper.Map<SaveMedicineResource, Medicine>(resource);
        var result = await _medicineService.UpdateAsync(id, medicine);

        if (!result.Success)
            return BadRequest(result.Message);

        var medicineResource = _mapper.Map<Medicine, MedicineResource>(result.Resource);
        return Ok(medicineResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _medicineService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var medicineResource = _mapper.Map<Medicine, MedicineResource>(result.Resource);
        return Ok(medicineResource);
    }
}