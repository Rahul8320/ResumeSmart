using ResumeSmart.Api.Models.Responses;

namespace ResumeSmart.Api.Providers.Interfaces;

/// <summary>
/// Represent token provider interface
/// </summary>
public interface ITokenProvider
{
    /// <summary>
    /// Generate access token
    /// </summary>
    /// <param name="user">User data</param>
    /// <returns>Returns newly generated access token</returns>
    string GenerateAccessToken(UserResponse user);
}