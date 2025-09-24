using Microsoft.AspNetCore.Http;
using Serilog.Context;
using Template.CrossCutting.ConfigTypes;

namespace Template.Bootstrap.Middlewares;

public class HttpRequestMiddleware
{
    private readonly RequestDelegate _next;

    public HttpRequestMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context,
        DefaultParams defaultParam)
    {
        var xRequestId = context.Request.Headers["x-request-id"].FirstOrDefault() ?? string.Empty;
        var xClientRequestId = context.Request.Headers["x-client-request-id"].FirstOrDefault() ?? string.Empty;

        defaultParam.RequestId = xRequestId;
        defaultParam.ClientRequestId = xClientRequestId;

        using (LogContext.PushProperty("x-request-id", xRequestId))
        using (LogContext.PushProperty("x-client-request-id", xClientRequestId))
        {
            await _next(context);
        }
    }
}
