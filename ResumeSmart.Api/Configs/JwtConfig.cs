namespace ResumeSmart.Api.Configs;

/// <summary>
/// Represent JWT config values
/// </summary>
public sealed class JwtConfig
{
    /// <summary>
    /// Gets JWT signin key value
    /// </summary>
    public required string Key { get; init; }

    /// <summary>
    /// Gets JWT issue value
    /// </summary>
    public required string Issuer { get; init; }

    /// <summary>
    /// Gets JWT audience value
    /// </summary>
    public required string Audience { get; init; }

    /// <summary>
    /// Gets access token expires in days value
    /// </summary>
    public required int AccessTokenExpiresInDays { get; init; }
}