using Elastic.Apm;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Template.Bootstrap.Middlewares;

public class HttpResponseMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<HttpResponseMiddleware> _logger;

    public HttpResponseMiddleware(RequestDelegate next, ILogger<HttpResponseMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.OnStarting(() =>
        {
            AddTraceHeaders(context);
            AddRequestIdHeaders(context);
            return Task.CompletedTask;
        });

        await _next(context);
    }

    private static void AddTraceHeaders(HttpContext context)
    {
        var transaction = Agent.Tracer.CurrentTransaction;
        if (transaction != null)
        {
            context.Response.Headers["x-trace-id"] = transaction.TraceId.ToString();
        }
    }

    private static void AddRequestIdHeaders(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue("x-request-id", out var xRequestId))
        {
            context.Response.Headers["x-request-id"] = xRequestId;
        }

        if (context.Request.Headers.TryGetValue("x-client-request-id", out var xClientRequestId))
        {
            context.Response.Headers["x-client-request-id"] = xClientRequestId;
        }
    }
}
