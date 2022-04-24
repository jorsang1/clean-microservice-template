﻿using Api.Endpoints;

namespace Api;

public static class DependencyInjection
{
    private static readonly ProductEndpoints productEndpoints = new ProductEndpoints();
    private static readonly SwaggerEndpoints swaggerEndpoints = new SwaggerEndpoints();
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        swaggerEndpoints.DefineServices(services);
        productEndpoints.DefineServices(services);

        //services.AddHttpLogging(httpLogging =>
        //{
        //    httpLogging.LoggingFields = HttpLoggingFields.All;
        //    httpLogging.MediaTypeOptions.AddText("application/javascript");
        //});

        return services;
    }

    public static WebApplication UseApi(this WebApplication app)
    {
        swaggerEndpoints.DefineEndpoints(app);
        productEndpoints.DefineEndpoints(app);

        app.UseHttpLogging();

        return app;
    }
}