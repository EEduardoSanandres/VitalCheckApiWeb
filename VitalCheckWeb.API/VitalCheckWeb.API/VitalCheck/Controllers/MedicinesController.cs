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
[SwaggerTag("Create, read, update and delete Medicine")]
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
    [ProducesResponseType(typeof(IEnumerable<MedicineResource>), 200)]
    [ProducesResponseType(500)]
    [SwaggerOperation(
        Summary = "Get All Medicines",
        Description = "Get a list of all Medicines",
        OperationId = "GetAllMedicines",
        Tags = new[] { "Medicines" }
    )]
    public async Task<IEnumerable<MedicineResource>> GetAllAsync()
    {
        var medicines = await _medicineService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Medicine>, IEnumerable<MedicineResource>>(medicines);
        return resources;
    }

    [HttpGet("medicineType/{medicineTypeId}")]
    [ProducesResponseType(typeof(IEnumerable<MedicineResource>), 200)]
    [ProducesResponseType(500)]
    [SwaggerOperation(
        Summary = "Get Medicines by Medicine Type ID",
        Description = "Get a list of Medicines where Medicine Type's ID matches the provided ID",
        OperationId = "GetMedicinesByMedicineTypeId",
        Tags = new[] { "Medicines" }
    )]
    public async Task<IEnumerable<MedicineResource>> GetByMedicineTypeIdAsync(int medicineTypeId)
    {
        var medicines = await _medicineService.ListByMedicineTypeIdAsync(medicineTypeId);
        var resources = _mapper.Map<IEnumerable<Medicine>, IEnumerable<MedicineResource>>(medicines);
        return resources;
    }

    [HttpPost]
    [ProducesResponseType(typeof(MedicineResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    [SwaggerOperation(
        Summary = "Create a new Medicine",
        Description = "Create a new Medicine with the specified information",
        OperationId = "CreateMedicine",
        Tags = new[] { "Medicines" }
    )]
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
    [ProducesResponseType(typeof(MedicineResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    [SwaggerOperation(
        Summary = "Update an existing Medicine",
        Description = "Update an existing Medicine with the specified ID and information",
        OperationId = "UpdateMedicine",
        Tags = new[] { "Medicines" }
    )]
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
    [ProducesResponseType(typeof(MedicineResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    [SwaggerOperation(
        Summary = "Delete an existing Medicine",
        Description = "Delete an existing Medicine with the specified ID",
        OperationId = "DeleteMedicine",
        Tags = new[] { "Medicines" }
    )]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _medicineService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var medicineResource = _mapper.Map<Medicine, MedicineResource>(result.Resource);
        return Ok(medicineResource);
    }
}