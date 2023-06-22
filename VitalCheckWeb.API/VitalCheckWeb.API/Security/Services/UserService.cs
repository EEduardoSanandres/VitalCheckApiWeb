using AutoMapper;
using VitalCheckWeb.API.Security.Authorization.Handlers.Interfaces;
using VitalCheckWeb.API.Security.Domain.Models;
using VitalCheckWeb.API.Security.Domain.Services;
using VitalCheckWeb.API.Security.Domain.Services.Communication;
using VitalCheckWeb.API.Security.Exceptions;
using VitalCheckWeb.API.VitalCheck.Domain.Repositories;
using BCryptNet = BCrypt.Net.BCrypt;

namespace VitalCheckWeb.API.Security.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IJwtHandler _jwtHandler;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IJwtHandler jwtHandler, IMapper mapper)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _jwtHandler = jwtHandler;
        _mapper = mapper;
    }

    public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
    {
        var user = await _userRepository.FindByUsernameAsync(request.UserName);
        Console.WriteLine($"Request: {request.UserName}, {request.Password}");
        Console.WriteLine($"User: {user.UserID}, {user.UserName}, {user.Email}, {user.RUC}, {user.Password}");
 
        // validate
        if (user == null || !BCryptNet.Verify(request.Password, user.Password))
        {
            Console.WriteLine("Authentication Error");
            throw new AppException("Username or password is incorrect");
        }
 
        Console.WriteLine("Authentication successful. About to generate token");
        // authentication successful
        var response = _mapper.Map<AuthenticateResponse>(user);
        Console.WriteLine($"Response: {response.UserID}, {response.UserName}, {response.Email}, {response.Token}");
        response.Token = _jwtHandler.GenerateToken(user);
        Console.WriteLine($"Generated token is {response.Token}");
        return response;
    }

    public async Task<IEnumerable<User>> ListAsync()
    {
        return await _userRepository.ListAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        var user = await _userRepository.FindByIdAsync(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }

    private User GetById(int id)
    {
        var user = _userRepository.FindById(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }

    public async Task RegisterAsync(RegisterRequest request)
    {
        // validate
        if (_userRepository.ExistsByUsername(request.UserName)) 
            throw new AppException("Username '" + request.UserName + "' is already taken");
        // map model to new user object
        var user = _mapper.Map<User>(request);
        // hash password
        user.Password = BCryptNet.HashPassword(request.Password);
        // save user
        try
        {
            await _userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while saving the user: {e.Message}");
        }
    }

    public async Task UpdateAsync(int id, UpdateRequest request)
    {
        var user = GetById(id);
        // Validate
        if (_userRepository.ExistsByUsername(request.UserName)) 
            throw new AppException("Username '" + request.UserName + "' is already taken");

        // Copy model to user
        _mapper.Map(request, user);

        // Hash password if it was entered
        if (!string.IsNullOrEmpty(request.Password))
            user.Password = BCryptNet.HashPassword(request.Password);

        try
        {
            _userRepository.Update(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while updating the user: {e.Message}");
        }
    }


    public async Task DeleteAsync(int id)
    {
        var user = GetById(id);
 
        try
        {
            _userRepository.Remove(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while deleting the user: {e.Message}");
        }
    }
}