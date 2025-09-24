using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Template.Bootstrap.Handlers;
using Template.Bootstrap.Middlewares;
using Template.CrossCutting.ConfigTypes;

namespace Template.Bootstrap.Configurations;

[ExcludeFromCodeCoverage]
public static class MiddlewareConfiguration
{
    public static IServiceCollection ConfigureMiddlewares(
       this IServiceCollection services)
    {
        services.AddScoped<DefaultParams>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }

    public static IApplicationBuilder ConfigureMiddlewares(this IApplicationBuilder app)
    {
        app.UseExceptionHandler();
        app.UseMiddleware<HttpRequestMiddleware>();
        app.UseMiddleware<HttpResponseMiddleware>();

        return app;
    }
}
