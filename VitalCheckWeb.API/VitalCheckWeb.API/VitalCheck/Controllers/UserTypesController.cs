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
[SwaggerTag("Create, read, update and delete User Type")]
public class UserTypesController : ControllerBase
{
    private readonly IUserTypeService _userTypeService;
    private readonly IMapper _mapper;

    public UserTypesController(IUserTypeService userTypeService, IMapper mapper)
    {
        _userTypeService = userTypeService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserTypeResource>), 200)]
    [ProducesResponseType(500)]
    [SwaggerOperation(
        Summary = "Get All User Type",
        Description = "Get a list of all User Types",
        OperationId = "GetAllUserType",
        Tags = new[] { "UserTypes" }
    )]
    public async Task<IEnumerable<UserTypeResource>> GetAllAsync()
    {
        var userTypes = await _userTypeService.ListAsync();
        var resources = _mapper.Map<IEnumerable<UserType>, IEnumerable<UserTypeResource>>(userTypes);
        return resources;
    }

    [HttpPost]
    [ProducesResponseType(typeof(UserTypeResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    [SwaggerOperation(
        Summary = "Create a new User Type",
        Description = "Create a new User Types with the specified information",
        OperationId = "CreateUserType",
        Tags = new[] { "UserTypes" }
    )]
    public async Task<IActionResult> CreateAsync([FromBody] SaveUserTypeResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userType = _mapper.Map<SaveUserTypeResource, UserType>(resource);
        var result = await _userTypeService.SaveAsync(userType);

        if (!result.Success)
            return BadRequest(result.Message);

        var userTypeResource = _mapper.Map<UserType, UserTypeResource>(result.Resource);
        return Ok(userTypeResource);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(UserTypeResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    [SwaggerOperation(
        Summary = "Update an existing User Type",
        Description = "Update an existing User Type with the specified ID and information",
        OperationId = "UpdateUserType",
        Tags = new[] { "UserTypes" }
    )]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] SaveUserTypeResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userType = _mapper.Map<SaveUserTypeResource, UserType>(resource);
        var result = await _userTypeService.UpdateAsync(id, userType);

        if (!result.Success)
            return BadRequest(result.Message);

        var userTypeResource = _mapper.Map<UserType, UserTypeResource>(result.Resource);
        return Ok(userTypeResource);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(UserTypeResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    [SwaggerOperation(
        Summary = "Delete an existing User Type",
        Description = "Delete an existing User Type with the specified ID",
        OperationId = "DeleteUserType",
        Tags = new[] { "UserTypes" }
    )]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _userTypeService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var userTypeResource = _mapper.Map<UserType, UserTypeResource>(result.Resource);
        return Ok(userTypeResource);
    }
}