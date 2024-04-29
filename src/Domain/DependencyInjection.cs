using CleanCompanyName.DDDMicroservice.Domain.Entities.Products;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CleanCompanyName.DDDMicroservice.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<Product>(includeInternalTypes: true);

        return services;
    }
}