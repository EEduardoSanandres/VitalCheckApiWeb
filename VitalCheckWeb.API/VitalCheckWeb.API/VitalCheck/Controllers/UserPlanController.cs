using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Services;
using VitalCheckWeb.API.VitalCheck.Resources;

namespace VitalCheckWeb.API.VitalCheck.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class UserPlanController : ControllerBase
{
    private readonly IUserPlanService _userPlanService;
    private readonly IMapper _mapper;

    public UserPlanController(IUserPlanService userPlanService, IMapper mapper)
    {
        _userPlanService = userPlanService;
        _mapper = mapper;
    }

    [HttpGet] 
    public async Task<IEnumerable<UserPlanResource>> GetAllAsync()
    {
        var userPlans = await _userPlanService.ListAsync();
        var resources = _mapper.Map<IEnumerable<UserPlan>, IEnumerable<UserPlanResource>>(userPlans);
        return resources;
    }

    [HttpPost]
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
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _userPlanService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var userPlanResource = _mapper.Map<UserPlan, UserPlanResource>(result.Resource);
        return Ok(userPlanResource);
    }
}