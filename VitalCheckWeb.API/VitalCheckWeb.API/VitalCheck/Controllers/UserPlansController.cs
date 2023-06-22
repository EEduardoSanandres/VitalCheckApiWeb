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
[SwaggerTag("Create, read, update and delete User Plan")]
public class UserPlansController : ControllerBase
{
    private readonly IUserPlanService _userPlanService;
    private readonly IMapper _mapper;

    public UserPlansController(IUserPlanService userPlanService, IMapper mapper)
    {
        _userPlanService = userPlanService;
        _mapper = mapper;
    }

    [HttpGet] 
    [ProducesResponseType(typeof(IEnumerable<UserPlanResource>), 200)]
    [ProducesResponseType(500)]
    [SwaggerOperation(
        Summary = "Get All User Plans",
        Description = "Get a list of all User Plans",
        OperationId = "GetAllUserPlans",
        Tags = new[] { "UserPlans" }
    )]
    public async Task<IEnumerable<UserPlanResource>> GetAllAsync()
    {
        var userPlans = await _userPlanService.ListAsync();
        var resources = _mapper.Map<IEnumerable<UserPlan>, IEnumerable<UserPlanResource>>(userPlans);
        return resources;
    }

    [HttpPost]
    [ProducesResponseType(typeof(UserPlanResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    [SwaggerOperation(
        Summary = "Create a new User Plan",
        Description = "Create a new User Plan with the specified information",
        OperationId = "CreateUserPlan",
        Tags = new[] { "UserPlans" }
    )]
    public async Task<IActionResult> CreateAsync([FromBody] SaveUserPlanResource resource)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);

        var userPlan = _mapper.Map<SaveUserPlanResource, UserPlan>(resource);
        var result = await _userPlanService.SaveAsync(userPlan);

        if (!result.Success)
            return BadRequest(result.Message);

        var userPlanResource = _mapper.Map<UserPlan, UserPlanResource>(result.Resource);
        return Ok(userPlanResource);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(UserPlanResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    [SwaggerOperation(
        Summary = "Update an existing User Plan",
        Description = "Update an existing User Plan with the specified ID and information",
        OperationId = "UpdateUserPlan",
        Tags = new[] { "UserPlans" }
    )]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] SaveUserPlanResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userPlan = _mapper.Map<SaveUserPlanResource, UserPlan>(resource);
        var result = await _userPlanService.UpdateAsync(id, userPlan);

        if (!result.Success)
            return BadRequest(result.Message);

        var userPlanResource = _mapper.Map<UserPlan, UserPlanResource>(result.Resource);
        return Ok(userPlanResource);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(UserPlanResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    [SwaggerOperation(
        Summary = "Delete an existing User Plan",
        Description = "Delete an existing User Plan with the specified ID",
        OperationId = "DeleteUserPlan",
        Tags = new[] { "UserPlans" }
    )]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _userPlanService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var userPlanResource = _mapper.Map<UserPlan, UserPlanResource>(result.Resource);
        return Ok(userPlanResource);
    }
}