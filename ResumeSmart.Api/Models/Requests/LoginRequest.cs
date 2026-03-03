using System.ComponentModel.DataAnnotations;

namespace ResumeSmart.Api.Models.Requests;

/// <summary>
/// Initialize login request object
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// Get or set email value
    /// </summary>
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is invalid")]
    public string Email { get; set; } = string.Empty;
    
    /// <summary>
    /// Get or sets password value
    /// </summary>
    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    [MaxLength(16, ErrorMessage = "Password must be 16 characters")]
    public string Password { get; set; } = string.Empty;
}