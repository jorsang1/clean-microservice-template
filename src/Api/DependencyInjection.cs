using CleanCompanyName.DDDMicroservice.Api.Endpoints;

namespace CleanCompanyName.DDDMicroservice.Api;

internal static class DependencyInjection
{
    private static readonly ProductEndpoints ProductEndpoints = new();
    private static readonly SwaggerEndpoints SwaggerEndpoints = new();

    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        //services.AddHttpLogging(httpLogging =>
        //{
        //    httpLogging.LoggingFields = HttpLoggingFields.All;
        //    httpLogging.MediaTypeOptions.AddText("application/javascript");
        //});

        return services;
    }

    public static WebApplication UseApi(this WebApplication app)
    {
        SwaggerEndpoints.DefineEndpoints(app);
        ProductEndpoints.DefineEndpoints(app);

        app.UseHttpLogging();

        return app;
    }
}