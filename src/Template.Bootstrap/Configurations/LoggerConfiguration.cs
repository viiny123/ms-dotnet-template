using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Elastic.Apm.SerilogEnricher;
using Elastic.Ingest.Elasticsearch;
using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
using Elastic.Transport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Enrichers.Span;
using Serilog.Events;
using Template.Bootstrap.Options;

namespace Template.Bootstrap.Configurations;

[ExcludeFromCodeCoverage]
public static class LoggerConfig
{
    public static IServiceCollection ConfigureLogger(this IServiceCollection services,
        IHostEnvironment environment,
        IConfiguration configuration)
    {
        var env = environment.EnvironmentName;
        var elasticOptions = new ElasticConfigurationOption();
        configuration.GetSection("ElasticConfiguration").Bind(elasticOptions);

        var loggerConfiguration = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithElasticApmCorrelationInfo()
            .Enrich.WithEnvironmentName()
            .Enrich.WithSpan(new SpanOptions
            {
                IncludeBaggage = true,
                IncludeOperationName = true,
                IncludeTags = true,
                LogEventPropertiesNames = new SpanLogEventPropertiesNames
                {
                    SpanId = "span.id",
                    ParentId = "parent.id",
                    TraceId = "trace.id"
                }
            })
            .WriteTo.Async(x =>
            {
                x.Elasticsearch(
                    nodes: [new Uri(elasticOptions.Uri)], configureOptions: opts =>
                    {
                        opts.BootstrapMethod = BootstrapMethod.Silent;
                        opts.DataStream = new DataStreamName($"dotnet8-{env.ToLower(CultureInfo.InvariantCulture)}-{ApiOption.SERVICE_NAME}");
                    },
                    transport =>
                    {
                        transport.Authentication(new BasicAuthentication(elasticOptions.User, elasticOptions.Password));
                        transport.ServerCertificateValidationCallback((o, certificate, arg3, arg4) => true);
                    },
                    restrictedToMinimumLevel: LogEventLevel.Information);
            });

        Log.Logger = loggerConfiguration.CreateLogger();

        return services;
    }
}
