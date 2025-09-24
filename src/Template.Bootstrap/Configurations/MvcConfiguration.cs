using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Template.Bootstrap.Configurations;

[ExcludeFromCodeCoverage]
public static class MvcConfiguration
{
    public static IServiceCollection ConfigureMvcServices(this IServiceCollection services)
    {
        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddControllers();
        services.Configure<JsonOptions>(options => options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never);
        services.AddControllers()
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        return services;
    }

    public static IApplicationBuilder ConfigureMvc(this IApplicationBuilder app)
    {
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseEndpoints(endpoints => endpoints.MapControllers());

        return app;
    }
}
