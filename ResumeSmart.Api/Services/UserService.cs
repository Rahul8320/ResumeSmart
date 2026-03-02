using MongoDB.Driver;
using MongoDB.Driver.Linq;
using ResumeSmart.Api.DB;
using ResumeSmart.Api.DB.Entities;
using ResumeSmart.Api.Models;
using ResumeSmart.Api.Services.Interfaces;

namespace ResumeSmart.Api.Services;

public class UserService(MongoDbContext dbContext, ILogger<UserService> logger): IUserService
{
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

    public async Task<IList<Users>> GetAllUsers()
    {
        logger.LogInformation("Fetching all users");
        var users = await dbContext.Users.AsQueryable().ToListAsync();
        return users;
    }
}