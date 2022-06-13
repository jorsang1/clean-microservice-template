using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mapster;
using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Infrastructure.Database.Repositories;
using CleanCompanyName.DDDMicroservice.Infrastructure.Database.Services;
using CleanCompanyName.DDDMicroservice.Infrastructure.Clients.StockClient;
using CleanCompanyName.DDDMicroservice.Infrastructure.Clients.StockClient.Configuration;
using CleanCompanyName.DDDMicroservice.Infrastructure.Database.Models;

namespace CleanCompanyName.DDDMicroservice.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationSection configuration)
    {
        services.AddSingleton<IDateTime, DateTimeService>();
        services.AddSingleton<IProductRepository, ProductRepository>();
        services.AddSingleton<IStockClient>(provider
            => new StockClient(
                    configuration.GetSection("StockClientConfiguration").Get<StockClientConfiguration>()
                ));

        return services;
    }
}