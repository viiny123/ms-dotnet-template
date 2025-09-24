using Elastic.Apm;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Template.Bootstrap.Handlers;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    private readonly IWebHostEnvironment _env;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IWebHostEnvironment env)
    {
        _logger = logger;
        _env = env;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var transaction = Agent.Tracer.CurrentTransaction;
        
        exception.Data["x-trace-id"] = transaction?.TraceId.ToString();

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Internal Server Error",
            Detail = _env.IsDevelopment() ? exception.Message : "An unexpected error occurred",
            Instance = httpContext.Request.Path,
        };
        
        problemDetails.Extensions["traceId"] = transaction?.TraceId.ToString();
        problemDetails.Extensions["timestamp"] = DateTime.UtcNow;
        
        if (_env.IsDevelopment())
        {
            problemDetails.Extensions["exceptionType"] = exception.GetType().Name;
            problemDetails.Extensions["stackTrace"] = exception.StackTrace;
        }

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        
        transaction?.CaptureException(exception);
        _logger.LogError(exception, "Unexpected error occurred. TraceId: {TraceId}", 
            transaction?.TraceId.ToString());

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}