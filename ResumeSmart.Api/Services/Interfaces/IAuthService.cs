using ResumeSmart.Api.Models;
using ResumeSmart.Api.Models.Requests;
using ResumeSmart.Api.Models.Responses;

namespace ResumeSmart.Api.Services.Interfaces;

/// <summary>
/// Auth service interface for managing authentication
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Register new user
    /// </summary>
    /// <param name="request">Register user request</param>
    /// <returns>Returns an instance of auth response</returns>
    public Task<Result<AuthResponse>> RegisterUser(RegisterUserRequest request);
    
    /// <summary>
    /// Login user
    /// </summary>
    /// <param name="request">Login user request</param>
    /// <returns>Returns an instance of auth response</returns>
    public Task<Result<AuthResponse>> Login(LoginRequest request);
}