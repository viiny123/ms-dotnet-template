using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Template.Data;

namespace Template.Bootstrap.Configurations;

[ExcludeFromCodeCoverage]
public static class DatabaseBootstrap
{
    public static IServiceCollection ConfigureDatabases(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextPool<TemplateDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("Default"),
                b => b.MigrationsAssembly("Template.Data")
            ));
        
        return services;
    }

    public static IApplicationBuilder RunMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices
                              .GetRequiredService<IServiceScopeFactory>()
                              .CreateScope();

        var context = scope.ServiceProvider
                           .GetRequiredService<TemplateDbContext>();

        context.Database.Migrate();

        return app;
    }
}
