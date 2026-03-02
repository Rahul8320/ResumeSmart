using ResumeSmart.Api.DB.Entities;
using ResumeSmart.Api.Models;

namespace ResumeSmart.Api.Services.Interfaces;

/// <summary>
/// User service instance for managing users
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Create new user
    /// </summary>
    /// <param name="request">Create user request</param>
    /// <returns>Void</returns>
    public Task CreateUser(CreateUserRequest request);
    
    /// <summary>
    /// Get all users
    /// </summary>
    /// <returns>Returns list of users</returns>
    public Task<IList<Users>> GetAllUsers();
}