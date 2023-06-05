using System.Reflection;
using CleanCompanyName.DDDMicroservice.Application.Pipelines;
using CleanCompanyName.DDDMicroservice.Application.Common.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace CleanCompanyName.DDDMicroservice.Application;



public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(TracingEnrichingPipelineBehaviour<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionEnrichingPipelineBehaviour<,>));

        ApplicationMapperConfig.AddMappingConfigs();

        return services;
    }
}