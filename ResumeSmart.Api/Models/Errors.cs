namespace ResumeSmart.Api.Models;

/// <summary>
/// Defines the error types
/// </summary>
public struct ErrorType
{
    public const string Conflict = "Conflict";
    public const string NotFound = "NotFound";
    public const string Validation = "Validation";
    public const string BadRequest = "BadRequest";
    public const string Unauthorized = "Unauthorized";
}

/// <summary>
/// Initialized an error object
/// </summary>
/// <param name="Id">Error ID</param>
/// <param name="Type">Error type</param>
/// <param name="Description">Error Description</param>
public record Error(string Id, string Type, string Description);

/// <summary>
/// Represents a list of predefined errors
/// </summary>
public static class Errors
{
    /// <summary>
    /// Gets user already exist error
    /// </summary>
    public static Error UserAlreadyExist { get; } = new("UserAlreadyExist",
        ErrorType.Conflict,
        "User with this email already exists.");

    /// <summary>
    /// Gets user not exist error
    /// </summary>
    public static Error UserNotExist { get; } = new("UserNotExist",
        ErrorType.NotFound,
        "User with this email doesn't exists.");

    /// <summary>
    /// Gets invalid credentials
    /// </summary>
    public static Error InvalidCredentials { get; } = new("InvalidCredentials",
        ErrorType.Unauthorized,
        "Invalid credentials.");
}