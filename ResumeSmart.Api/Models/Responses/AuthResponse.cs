namespace ResumeSmart.Api.Models.Responses;

/// <summary>
/// Initialize auth response object
/// </summary>
/// <param name="Message">Success message</param>
/// <param name="User">User data</param>
/// <param name="Token">Authentication token</param>
public record AuthResponse(string Message, UserResponse User, string Token);

/// <summary>
/// Represents auth responses
/// </summary>
public static class AuthResponses
{
    /// <summary>
    /// User register response
    /// </summary>
    /// <param name="user">User data</param>
    /// <param name="token">Auth token</param>
    /// <returns>Returns an instance of auth response</returns>
    public static AuthResponse UserRegister(UserResponse user, string token) => new(
        Message: "User successfully registered",
        User: user,
        Token: token);

    /// <summary>
    /// Login response
    /// </summary>
    /// <param name="user">User data</param>
    /// <param name="token">Auth token</param>
    /// <returns>returns an instance of auth response</returns>
    public static AuthResponse Login(UserResponse user, string token) => new(
        Message: "Login successful",
        User: user,
        Token: token);
}