using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Template.Data.Base;
using Template.Data.Repositories;
using Template.Domain.AggregatesModel.ValueAggreate;
using Template.Domain.Base;

namespace Template.Bootstrap.Configurations;

[ExcludeFromCodeCoverage]
public static class ApiConfiguration
{
    public static IServiceCollection ConfigureApiDependecies(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IValueRepository, ValueRepository>();

        return services;
    }
}
