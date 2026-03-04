namespace ResumeSmart.Api.Providers.Interfaces;

/// <summary>
/// Represent password hasher interface
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Hash plain text password with salt
    /// </summary>
    /// <param name="password">The plain text password value</param>
    /// <returns>Return hashed password</returns>
    string HashPassword(string password);
    
    /// <summary>
    /// Verify plain text password with hashed value
    /// </summary>
    /// <param name="hashedPassword">The hashed value</param>
    /// <param name="password">The plain text password</param>
    /// <returns>Return true if verified else false</returns>
    bool VerifyHashedPassword(string hashedPassword, string password);
}