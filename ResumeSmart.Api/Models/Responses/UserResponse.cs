using ResumeSmart.Api.DB.Entities;

namespace ResumeSmart.Api.Models.Responses;

/// <summary>
/// Initialize user response object
/// </summary>
/// <param name="Name">User's name</param>
/// <param name="Email">User's email</param>
public record UserResponse(string Id, string Name, string Email);

/// <summary>
/// User response extensions
/// </summary>
public static class UserResponseExtensions
{
    /// <summary>
    /// Map user to user response
    /// </summary>
    /// <param name="user">User data</param>
    /// <returns>Return user response object</returns>
    public static UserResponse ToUserResponse(this User user) => new(user.Id.ToString(), user.Name, user.Email);
}