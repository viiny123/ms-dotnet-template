using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Serilog;
using Template.Bootstrap.Configurations;

namespace Template.Bootstrap;

[ExcludeFromCodeCoverage]
public static class ServicesConfiguration
{
    public static WebApplicationBuilder ConfigureApplicationBuilder(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var environment = builder.Environment;
        var configuration = builder.Configuration;

        _ = services.ConfigureMvcServices();
        _ = services.ConfigureMiddlewares();
        _ = services.ConfigureSwagger();

        _ = services.ConfigureHealthCheck(configuration);
        _ = services.ConfigureLogger(environment, configuration);
        _ = builder.Host.UseSerilog();
        _ = services.ConfigureMediatr();
        _ = services.ConfigureDatabases(configuration);
        _ = services.ConfigureApiDependecies();

        return builder;
    }

    public static WebApplication ConfigureWebApplication(this WebApplication app)
    {
        app.ConfigureMiddlewares();
        app.ConfigureMvc();
        app.ConfigureHealthCheck();
        app.ConfigureSwagger();
        app.UseAuthorization();
        app.RunMigrations();

        return app;
    }
}
