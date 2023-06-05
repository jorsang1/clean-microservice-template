using CleanCompanyName.DDDMicroservice.Api.Endpoints;
using CleanCompanyName.DDDMicroservice.Api.Middleware;
using CleanCompanyName.DDDMicroservice.Api.Telemetry;
using OpenTelemetry.Trace;
using OpenTelemetry.Resources;
using OpenTelemetry.Metrics;
using OpenTelemetry.Instrumentation.AspNetCore;

namespace CleanCompanyName.DDDMicroservice.Api;

internal static class DependencyInjection
{
    private const string ServiceName = "CleanMicroserviceTemplate";
    private static readonly ProductEndpoints ProductEndpoints = new();
    private static readonly SwaggerEndpoints SwaggerEndpoints = new();

    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddHttpLogging(httpLogging =>
        {
            httpLogging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
        });

        services.AddOpenTelemetry().WithMetrics(options =>
            options.AddHttpClientInstrumentation()
                .AddAspNetCoreInstrumentation()
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(ServiceName).AddTelemetrySdk())
                .AddOtlpExporter(otlpOption =>
                {
                    otlpOption.Endpoint = new Uri("http://your-open-collector-instance:4317", UriKind.Absolute);
                })
                .AddPrometheusExporter()
                .AddMeter(ApplicationMetrics.ServiceMetricName)
        )
            .WithTracing(options =>
            options
                .AddSource(ServiceName)
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(ServiceName).AddTelemetrySdk())
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                // Enable the OTLP Exporter when it is ready otherwise it breaks the pipeline and you won't see any traces on the console either.
                // .AddOtlpExporter(otlpOption =>
                // {
                //     otlpOption.Endpoint = new Uri("http://agent.grafana-cloud.svc.cluster.local:4317", UriKind.Absolute);
                //     // For OTLP only gRPC works and you need to specify http:// if you don't want to use TLS.
                // })
                .AddConsoleExporter()
        );

        services.Configure<AspNetCoreInstrumentationOptions>(options =>
            {
                options.RecordException = true;
            }
        );

        return services;
    }

    public static WebApplication UseApi(this WebApplication app)
    {
        SwaggerEndpoints.DefineEndpoints(app);
        ProductEndpoints.DefineEndpoints(app);

        app.UseOpenTelemetryPrometheusScrapingEndpoint();
        app.UseHttpLogging();
        app.UseMiddleware<ErrorHandlerMiddleware>();

        return app;
    }
}