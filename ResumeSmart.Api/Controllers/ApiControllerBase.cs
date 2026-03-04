using Microsoft.AspNetCore.Mvc;
using ResumeSmart.Api.Models;

namespace ResumeSmart.Api.Controllers;

/// <summary>
/// Represents api controller base class
/// </summary>
[ApiController]
public class ApiControllerBase : ControllerBase
{
    /// <summary>
    /// Handle failure results
    /// </summary>
    /// <param name="result">The result object</param>
    /// <returns>Returns an instance of IActionResult</returns>
    protected IActionResult HandleFailure(Result result)
    {
        if (result.Error == null)
            return StatusCode(500, "An unknown error occurred.");

        return result.Error.Type switch
        {
            ErrorType.Unauthorized => Unauthorized(result.Error),
            ErrorType.Conflict => Conflict(result.Error),
            ErrorType.NotFound => NotFound(result.Error),
            ErrorType.Validation or ErrorType.BadRequest => BadRequest(result.Error),
            _ => StatusCode(500, result.Error)
        };
    }
}
