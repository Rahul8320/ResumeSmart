using Microsoft.AspNetCore.Mvc;
using ResumeSmart.Api.Models.Requests;
using ResumeSmart.Api.Services.Interfaces;

namespace ResumeSmart.Api.Controllers;

/// <summary>
/// Initialize auth controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService, ILogger<AuthController> logger) : ApiControllerBase
{
    /// <summary>
    /// Register new user endpoint
    /// </summary>
    /// <param name="request">Register user request</param>
    /// <returns>Newly created user data with token</returns>
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
        logger.LogInformation("Registering user. Request: {@Request}", request);

        if (!ModelState.IsValid)
        {
            logger.LogWarning("Request invalid. Request: {@Request}, State: {@ModelState}", request,
                ModelState);
            return BadRequest(ModelState);
        }
        
        var result = await authService.RegisterUser(request);

        if (!result.IsSuccess)
        {
            logger.LogWarning("Register failed. Result: {@Result}", result);
            return HandleFailure(result);
        }
        
        return Ok(result);
    }
}