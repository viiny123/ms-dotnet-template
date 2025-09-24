using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Template.Bootstrap.Behaviors;

namespace Template.Bootstrap.Configurations;

[ExcludeFromCodeCoverage]
public static class MediatorConfiguration
{

    private const string APPLICATION_NAMESPACE = "Template.Application";

    public static IServiceCollection ConfigureMediatr(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.Load(APPLICATION_NAMESPACE)));

        var currentAssemblies = AppDomain.CurrentDomain.GetAssemblies()
        .Where(c => c.GetName().Name.Contains(APPLICATION_NAMESPACE));

        AssemblyScanner.FindValidatorsInAssemblies(currentAssemblies)
          .ForEach(c => services.AddScoped(c.InterfaceType, c.ValidatorType));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidatorBehavior<,>));

        return services;
    }
}
