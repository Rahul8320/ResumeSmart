using MongoDB.Driver;
using MongoDB.Driver.Linq;
using ResumeSmart.Api.DB;
using ResumeSmart.Api.DB.Entities;
using ResumeSmart.Api.Models;
using ResumeSmart.Api.Services.Interfaces;

namespace ResumeSmart.Api.Services;

/// <summary>
/// Initialize user service instance
/// </summary>
/// <param name="dbContext">Mongo db context</param>
/// <param name="logger">Logger</param>
public class UserService(MongoDbContext dbContext, ILogger<UserService> logger): IUserService
{
    /// <summary>
    /// Create new user
    /// </summary>
    /// <param name="request">Create user request</param>
    /// <returns>Void</returns>
    public async Task CreateUser(CreateUserRequest request)
    {
        logger.LogInformation("Creating user for name: {name}, email: {email}", request.Name, request.Email);
        var user = new Users
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password
        };
        
        await dbContext.Users.InsertOneAsync(user);
        logger.LogInformation("User created: {name}, Id: {id}", user.Name, user.Id);
    }

    /// <summary>
    /// Get all users
    /// </summary>
    /// <returns>Returns list of users</returns>
    public async Task<IList<Users>> GetAllUsers()
    {
        logger.LogInformation("Fetching all users");
        var users = await dbContext.Users.AsQueryable().ToListAsync();
        return users;
    }
}