using System.Net;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Base;

namespace Template.API.Base.Presenters;

public static class ApiResponseFactory
{
    public static IActionResult Cast(object result, HttpStatusCode statusCode)
    {
        return Cast(new Result { Data = result }, statusCode);
    }

    public static IActionResult Cast(Result result, HttpStatusCode statusCode)
    {
        if (!result.IsValid)
        {
            return result.StatusCode.Value switch
            {
                StatusCodes.Status400BadRequest => new BadRequestObjectResult(GetError(result)),
                StatusCodes.Status401Unauthorized => new UnauthorizedObjectResult(GetError(result)),
                StatusCodes.Status403Forbidden => new ObjectResult(GetError(result))
                {
                    StatusCode = StatusCodes.Status403Forbidden
                },
                StatusCodes.Status404NotFound => new NotFoundObjectResult(GetError(result)),
                StatusCodes.Status409Conflict => new ConflictObjectResult(GetError(result)),
                StatusCodes.Status422UnprocessableEntity => new UnprocessableEntityObjectResult(GetError(result)),
                StatusCodes.Status500InternalServerError => new ObjectResult(GetError(result))
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                },
                StatusCodes.Status503ServiceUnavailable => new ObjectResult(GetError(result))
                {
                    StatusCode = StatusCodes.Status503ServiceUnavailable
                },
                _ => new ObjectResult(GetError(result))
                {
                    StatusCode = result.StatusCode.Value
                }
            };
        }
        
        return statusCode switch
        {
            HttpStatusCode.OK => new OkObjectResult(result.Data),
            HttpStatusCode.Created => new CreatedResult(string.Empty, result.Data),
            HttpStatusCode.NoContent => new NoContentResult(),
            HttpStatusCode.Accepted => new AcceptedResult(string.Empty, result.Data),
            _ => new OkResult()
        };
    }

    private static ValidationProblemDetails GetError(Result result)
    {
        var problemDetails = new ValidationProblemDetails
        {
            Status = result.StatusCode ?? StatusCodes.Status400BadRequest,
            Title = GetTitle(result.StatusCode ?? StatusCodes.Status400BadRequest),
            Detail = "One or more validation errors occurred"
        };
        
        foreach (var error in result.Errors)
        {
            var key = error.Property;
            var value = string.IsNullOrWhiteSpace(error.ErrorCode) 
                ? $"{error.Message}" 
                : $"{error.ErrorCode}: {error.Message}";
            
            if (problemDetails.Errors.ContainsKey(key))
            {
                var existingValues = problemDetails.Errors[key].ToList();
                existingValues.Add(value);
                problemDetails.Errors[key] = existingValues.ToArray();
            }
            else
            {
                problemDetails.Errors.Add(key, [value]);
            }
        }

        return problemDetails;
    }

    private static string GetTitle(int statusCode)
    {
        return statusCode switch
        {
            StatusCodes.Status400BadRequest => "Bad Request",
            StatusCodes.Status401Unauthorized => "Unauthorized",
            StatusCodes.Status403Forbidden => "Forbidden",
            StatusCodes.Status404NotFound => "Not Found",
            StatusCodes.Status409Conflict => "Conflict",
            StatusCodes.Status422UnprocessableEntity => "Unprocessable Entity",
            StatusCodes.Status500InternalServerError => "Internal Server Error",
            StatusCodes.Status503ServiceUnavailable => "Service Unavailable",
            _ => "Error"
        };
    }
}
