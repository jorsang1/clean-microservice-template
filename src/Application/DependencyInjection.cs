using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace CleanCompanyName.DDDMicroservice.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }
}