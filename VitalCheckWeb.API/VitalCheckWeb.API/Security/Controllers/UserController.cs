using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VitalCheckWeb.API.Security.Authorization.Attributes;
using VitalCheckWeb.API.Security.Domain.Models;
using VitalCheckWeb.API.Security.Domain.Services;
using VitalCheckWeb.API.Security.Domain.Services.Communication;
using VitalCheckWeb.API.Security.Resources;

namespace VitalCheckWeb.API.Security.Controllers;

[Authorize]
[ApiController]
[Route("/api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Sign-Up, Sign-in, read, update and delete Users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService; 
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpPost("sign-in")]
    [ProducesResponseType(typeof(AuthenticateResponse), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    [SwaggerOperation(
        Summary = "User Authentication",
        Description = "Authenticate a user with the specified credentials",
        OperationId = "UserAuthenticate",
        Tags = new[] { "User" }
    )]
    public async Task<IActionResult> Authenticate(AuthenticateRequest request)
    {
        var response = await _userService.Authenticate(request);
        return Ok(response);
    }
    
    [AllowAnonymous]
    [HttpPost("sign-up")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    [SwaggerOperation(
        Summary = "User Registration",
        Description = "Register a new user with the specified details",
        OperationId = "UserRegister",
        Tags = new[] { "User" }
    )]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        await _userService.RegisterAsync(request);
        return Ok(new { message = "Registration successful" });
    }
    
    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserResource>), 200)]
    [ProducesResponseType(500)]
    [SwaggerOperation(
        Summary = "Get All Users",
        Description = "Get a list of all users",
        OperationId = "GetAllUsers",
        Tags = new[] { "User" }
    )]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.ListAsync();
        var resources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);
        return Ok(resources);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UserResource), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [SwaggerOperation(
        Summary = "Get User By ID",
        Description = "Get a user with the specified ID",
        OperationId = "GetUserById",
        Tags = new[] { "User" }
    )]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        var resource = _mapper.Map<User, UserResource>(user);
        return Ok(resource);
    }

    [AllowAnonymous]
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    [SwaggerOperation(
        Summary = "Update User",
        Description = "Update a user with the specified ID and details",
        OperationId = "UpdateUser",
        Tags = new[] { "User" }
    )]
    public async Task<IActionResult> Update(int id, UpdateRequest request)
    {
        await _userService.UpdateAsync(id, request);
        return Ok(new { message = "User updated successfully" });
    }
    
    [AllowAnonymous]
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    [SwaggerOperation(
        Summary = "Delete User",
        Description = "Delete a user with the specified ID",
        OperationId = "DeleteUser",
        Tags = new[] { "User" }
    )]
    public async Task<IActionResult> Delete(int id)
    {
        await _userService.DeleteAsync(id);
        return Ok(new { message = "User deleted successfully" });
    }
}