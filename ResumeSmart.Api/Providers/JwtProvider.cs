using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ResumeSmart.Api.Configs;
using ResumeSmart.Api.Models.Responses;
using ResumeSmart.Api.Providers.Interfaces;

namespace ResumeSmart.Api.Providers;

/// <summary>
/// Represent JWT provider
/// </summary>
public sealed class JwtProvider(IOptions<JwtConfig> jwtConfig) : ITokenProvider
{
    /// <summary>
    /// Represent jwt config values
    /// </summary>
    private readonly JwtConfig _jwtConfig = jwtConfig.Value;

    /// <summary>
    /// Represents security algorithm value
    /// </summary>
    private const string SecurityAlgorithm = SecurityAlgorithms.HmacSha512;

    /// <summary>
    /// Generate access token
    /// </summary>
    /// <param name="user">User data</param>
    /// <returns>Returns newly generated access token</returns>
    public string GenerateAccessToken(UserResponse user)
    {
        var key = Encoding.UTF8.GetBytes(_jwtConfig.Key);
        var securityKey = new SymmetricSecurityKey(key);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithm);
        var expiresIn = DateTime.UtcNow.AddDays(_jwtConfig.AccessTokenExpiresInDays);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.Name),
            new(ClaimTypes.Email, user.Email),
        };

        // claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var token = new JwtSecurityToken(
            issuer: _jwtConfig.Issuer,
            audience: _jwtConfig.Audience,
            claims: claims,
            expires: expiresIn,
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}