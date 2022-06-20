using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Infrastructure.Database.Repositories;
using CleanCompanyName.DDDMicroservice.Infrastructure.Database.Services;
using CleanCompanyName.DDDMicroservice.Infrastructure.Clients.StockClient;
using CleanCompanyName.DDDMicroservice.Infrastructure.Clients.StockClient.Configuration;

namespace CleanCompanyName.DDDMicroservice.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationSection configuration)
    {
        services.Configure<StockClientConfiguration>(configuration.GetSection("StockClientConfiguration"));

        services.AddSingleton<IDateTime, DateTimeService>();
        services.AddSingleton<IProductRepository, ProductRepository>();
        services.AddTransient<IStockClient, StockClient>();

        services.AddHttpClient<StockClient>();

        return services;
    }
}