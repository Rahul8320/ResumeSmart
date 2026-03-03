namespace ResumeSmart.Api.Models;

public struct ErrorType
{
    public const string Conflict = "Conflict";
    public const string NotFound = "NotFound";
    public const string Validation = "Validation";
    public const string BadRequest = "BadRequest";
    public const string Unauthorized = "Unauthorized";
}

public record Error(string Id, string Type, string Description);

public static class Errors
{
    public static Error UserAlreadyExist { get; } = new("UserAlreadyExist",
        ErrorType.Conflict,
        "User with this email already exists.");

    public static Error InsufficientFunds { get; } = new("InsufficientFunds",
        ErrorType.Validation,
        "Insufficient balance.");
}