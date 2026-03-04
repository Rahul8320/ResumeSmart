using DevOne.Security.Cryptography.BCrypt;
using ResumeSmart.Api.Providers.Interfaces;

namespace ResumeSmart.Api.Providers;

/// <summary>
/// Implementation of IPasswordHasher interface using BCrypt 
/// </summary>
public sealed class PasswordHasher : IPasswordHasher
{
    /// <summary>
    /// Hash plain text password with salt using BCrypter
    /// </summary>
    /// <param name="password">The plain text password value</param>
    /// <returns>Return hashed password</returns>
    public string HashPassword(string password)
    {
        var salt = GenerateSalt();
        return BCryptHelper.HashPassword(password, salt);
    }

    /// <summary>
    /// Verify plain text password with hashed value
    /// </summary>
    /// <param name="hashedPassword">The hashed value</param>
    /// <param name="password">The plain text password</param>
    /// <returns>Return true if verified else false</returns>
    public bool VerifyHashedPassword(string hashedPassword, string password)
    {
        return BCryptHelper.CheckPassword(password, hashedPassword);
    }

    /// <summary>
    /// Generate salt
    /// </summary>
    /// <returns>Returns newly generated salt value</returns>
    private string GenerateSalt()
    {
        return BCryptHelper.GenerateSalt(12);
    }
}