using System.ComponentModel.DataAnnotations;

namespace ResumeSmart.Api.Models.Requests;

/// <summary>
/// Initialize register user request object
/// </summary>
public class RegisterUserRequest
{
    /// <summary>
    /// Get or set name value
    /// </summary>
    [Required(ErrorMessage = "Name is required")]
    [MinLength(3, ErrorMessage = "Name must be at least 3 characters")]
    [MaxLength(50, ErrorMessage = "Name must be 50 characters")]
    public string Name { get; set; } = string.Empty;
    
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