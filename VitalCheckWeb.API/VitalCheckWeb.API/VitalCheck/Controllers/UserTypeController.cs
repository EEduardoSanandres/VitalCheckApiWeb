using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Services;
using VitalCheckWeb.API.VitalCheck.Resources;

namespace VitalCheckWeb.API.VitalCheck.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class UserTypeController : ControllerBase
{
    private readonly IUserTypeService _userTypeService;
    private readonly IMapper _mapper;

    public UserTypeController(IUserTypeService userTypeService, IMapper mapper)
    {
        _userTypeService = userTypeService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<UserTypeResource>> GetAllAsync()
    {
        var userTypes = await _userTypeService.ListAsync();
        var resources = _mapper.Map<IEnumerable<UserType>, IEnumerable<UserTypeResource>>(userTypes);
        return resources;
    }

    [HttpPost]
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
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _userTypeService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var userTypeResource = _mapper.Map<UserType, UserTypeResource>(result.Resource);
        return Ok(userTypeResource);
    }
}