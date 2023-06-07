using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Services;
using VitalCheckWeb.API.VitalCheck.Resources;

namespace VitalCheckWeb.API.VitalCheck.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class MedicineTypesController : ControllerBase
{
    private readonly IMedicineTypeService _medicineTypeService;
    private readonly IMapper _mapper;

    public MedicineTypesController(IMedicineTypeService medicineTypeService, IMapper mapper)
    {
        _medicineTypeService = medicineTypeService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<MedicineTypeResource>> GetAllAsync()
    {
        var medicineTypes = await _medicineTypeService.ListAsync();
        var resources = _mapper.Map<IEnumerable<MedicineType>, IEnumerable<MedicineTypeResource>>(medicineTypes);
        return resources;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] SaveMedicineTypeResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var medicineType = _mapper.Map<SaveMedicineTypeResource, MedicineType>(resource);
        var result = await _medicineTypeService.SaveAsync(medicineType);

        if (!result.Success)
            return BadRequest(result.Message);

        var medicineTypeResource = _mapper.Map<MedicineType, MedicineTypeResource>(result.Resource);
        return Ok(medicineTypeResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] SaveMedicineTypeResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var medicineType = _mapper.Map<SaveMedicineTypeResource, MedicineType>(resource);
        var result = await _medicineTypeService.UpdateAsync(id, medicineType);

        if (!result.Success)
            return BadRequest(result.Message);

        var medicineTypeResource = _mapper.Map<MedicineType, MedicineTypeResource>(result.Resource);
            return Ok(medicineTypeResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _medicineTypeService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var medicineTypeResource = _mapper.Map<MedicineType, MedicineTypeResource>(result.Resource);
        return Ok(medicineTypeResource);
    }
}