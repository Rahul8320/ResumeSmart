using Microsoft.AspNetCore.Mvc;
using ResumeSmart.Api.Models;

namespace ResumeSmart.Api.Controllers;

[ApiController]
public class ApiControllerBase : ControllerBase
{
    protected IActionResult HandleFailure(Result result)
    {
        if (result.Error == null)
            return StatusCode(500, "An unknown error occurred.");

        return result.Error.Type switch
        {
            ErrorType.Conflict => Conflict(result.Error),
            ErrorType.NotFound => NotFound(result.Error),
            ErrorType.Validation => BadRequest(result.Error),
            ErrorType.BadRequest => BadRequest(result.Error),
            _ => StatusCode(500, result.Error)
        };
    }
}
