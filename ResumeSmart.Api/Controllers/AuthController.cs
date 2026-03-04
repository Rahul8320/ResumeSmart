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
    /// Register new user
    /// </summary>
    /// <param name="request">Register user request</param>
    /// <returns>Returns newly created user data with token</returns>
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
        try
        {
            logger.LogInformation("Registering user. Request: {@Request}", request);

            if (!ModelState.IsValid)
            {
                logger.LogWarning("Request invalid. Request: {@Request}, State: {@ModelState}", request,
                    ModelState);
                return BadRequest(ModelState);
            }

            var result = await authService.RegisterUser(request);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            logger.LogWarning("Register failed. Result: {@Result}", result);
            return HandleFailure(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Register failed. Request: {@Request}", request);
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Login user
    /// </summary>
    /// <param name="request">Login user request</param>
    /// <returns>Returns user data with token</returns>
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            logger.LogInformation("Logging user. Request: {@Request}", request);

            if (!ModelState.IsValid)
            {
                logger.LogWarning("Request invalid. Request: {@Request}, State: {@State}", request, ModelState);
                return BadRequest(ModelState);
            }

            var result = await authService.Login(request);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            logger.LogWarning("Login failed. Result: {@Result}", result);
            return HandleFailure(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Login failed. Request: {@Request}", request);
            return StatusCode(500, "Internal server error");
        }
    }
}