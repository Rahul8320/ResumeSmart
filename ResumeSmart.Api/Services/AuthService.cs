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
public class AuthService(
    MongoDbContext dbContext,
    IPasswordHasher passwordHasher,
    ILogger<AuthService> logger) : IAuthService
{
    /// <summary>
    /// Register new user
    /// </summary>
    /// <param name="request">Register user request</param>
    /// <returns>Returns an instance of auth response</returns>
    public async Task<Result<AuthResponse>> RegisterUser(RegisterUserRequest request)
    {
        logger.LogInformation("Fetching users with email {email}", request.Email);
        var existingUser = await GetUserByEmail(request.Email);

        if (existingUser != null)
        {
            logger.LogInformation("User with email {email} already exists", request.Email);
            return Errors.UserAlreadyExist;
        }

        var user = await CreateNewUser(request);
        logger.LogInformation("User with email {email} registered", user.Email);

        return AuthResponses.UserRegister(user.ToUserResponse(), "Token");
    }

    /// <summary>
    /// Login user
    /// </summary>
    /// <param name="request">Login user request</param>
    /// <returns>Returns an instance of auth response</returns>
    public async Task<Result<AuthResponse>> Login(LoginRequest request)
    {
        logger.LogInformation("Fetching users with email {email}", request.Email);
        var existingUser = await GetUserByEmail(request.Email);

        if (existingUser == null)
        {
            logger.LogWarning("User with email {email} not exist", request.Email);
            return Errors.UserNotExist;
        }

        var isValidPassword = passwordHasher.VerifyHashedPassword(existingUser.Password, request.Password);

        if (!isValidPassword)
        {
            logger.LogWarning("User with email {email} not authorized", request.Email);
            return Errors.InvalidCredentials;
        }

        return AuthResponses.Login(existingUser.ToUserResponse(), "Token");
    }

    /// <summary>
    /// Fetch user by email
    /// </summary>
    /// <param name="email">The email id</param>
    /// <returns>Returns user if found else null</returns>
    private async Task<User?> GetUserByEmail(string email)
    {
        var collation = new Collation(locale: "en", strength: CollationStrength.Secondary);

        return await dbContext.Users
            .Find(x => x.Email == email, new FindOptions { Collation = collation })
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Create new user and save to db
    /// </summary>
    /// <param name="request">The create new user request data</param>
    /// <returns>Returns the newly created user</returns>
    private async Task<User> CreateNewUser(RegisterUserRequest request)
    {
        var user = new User()
        {
            Name = request.Name,
            Email = request.Email,
            Password = passwordHasher.HashPassword(request.Password),
            CreatedAt = DateTime.UtcNow,
            ModifiedAt = DateTime.UtcNow
        };

        await dbContext.Users.InsertOneAsync(user);
        return user;
    }
}