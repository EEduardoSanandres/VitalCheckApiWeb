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
[SwaggerTag("Create, read, update and delete Medicine Type")]
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
    [ProducesResponseType(typeof(IEnumerable<MedicineTypeResource>), 200)]
    [ProducesResponseType(500)]
    [SwaggerOperation(
        Summary = "Get All Medicine Types",
        Description = "Get a list of all Medicine Types",
        OperationId = "GetAllMedicineTypes",
        Tags = new[] { "MedicineTypes" }
    )]
    public async Task<IEnumerable<MedicineTypeResource>> GetAllAsync()
    {
        var medicineTypes = await _medicineTypeService.ListAsync();
        var resources = _mapper.Map<IEnumerable<MedicineType>, IEnumerable<MedicineTypeResource>>(medicineTypes);
        return resources;
    }

    [HttpPost]
    [ProducesResponseType(typeof(MedicineTypeResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    [SwaggerOperation(
        Summary = "Create a new Medicine Type",
        Description = "Create a new Medicine Type with the specified information",
        OperationId = "CreateMedicineType",
        Tags = new[] { "MedicineTypes" }
    )]
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
    [ProducesResponseType(typeof(MedicineTypeResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    [SwaggerOperation(
        Summary = "Update an existing Medicine Type",
        Description = "Update an existing Medicine Type with the specified ID and information",
        OperationId = "UpdateMedicineType",
        Tags = new[] { "MedicineTypes" }
    )]
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
    [ProducesResponseType(typeof(MedicineTypeResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    [SwaggerOperation(
        Summary = "Delete an existing Medicine Type",
        Description = "Delete an existing Medicine Type with the specified ID",
        OperationId = "DeleteMedicineType",
        Tags = new[] { "MedicineTypes" }
    )]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _medicineTypeService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var medicineTypeResource = _mapper.Map<MedicineType, MedicineTypeResource>(result.Resource);
        return Ok(medicineTypeResource);
    }
}