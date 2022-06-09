using CleanCompanyName.DDDMicroservice.Api.Endpoints;
using CleanCompanyName.DDDMicroservice.Api.Middleware;

namespace CleanCompanyName.DDDMicroservice.Api;

internal static class DependencyInjection
{
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

        return services;
    }

    public static WebApplication UseApi(this WebApplication app)
    {
        SwaggerEndpoints.DefineEndpoints(app);
        ProductEndpoints.DefineEndpoints(app);

        app.UseHttpLogging();
        app.UseMiddleware<RequestScopeLoggingMiddleware>();

        return app;
    }
}