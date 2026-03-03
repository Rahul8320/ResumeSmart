using MongoDB.Driver;
using ResumeSmart.Api.DB;
using ResumeSmart.Api.DB.Entities;
using ResumeSmart.Api.Models;
using ResumeSmart.Api.Models.Requests;
using ResumeSmart.Api.Models.Responses;
using ResumeSmart.Api.Services.Interfaces;

namespace ResumeSmart.Api.Services;

/// <summary>
/// Initialize user service instance
/// </summary>
/// <param name="dbContext">Mongo db context</param>
/// <param name="logger">Logger</param>
public class AuthService(MongoDbContext dbContext, ILogger<AuthService> logger): IAuthService
{
    public async Task<Result<AuthResponse>> RegisterUser(RegisterUserRequest request)
    {
        logger.LogInformation("Fetching users with email {email}", request.Email);
        var existingUser = await dbContext.Users
            .FindAsync(x => x.Email == request.Email)
            .Result
            .FirstOrDefaultAsync();

        if (existingUser != null)
        {
            logger.LogInformation("User with email {email} already exists", request.Email);
            return Errors.UserAlreadyExist;
        }

        var user = new User()
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
        };
        
        await dbContext.Users.InsertOneAsync(user);
        logger.LogInformation("User with email {email} registered", request.Email);

        return AuthResponses.UserRegister(user.ToUserResponse(), "Token");
    }

    public Task<Result<AuthResponse>> Login(LoginRequest request)
    {
        throw new NotImplementedException();
    }
}