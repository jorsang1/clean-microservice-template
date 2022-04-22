using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Common.Interfaces;
using Infrastructure.Database.Repositories;
using Infrastructure.Database.Services;
using Mapster;
using Infrastructure.Clients.StockClient;
using Infrastructure.Clients.StockClient.Configuration;

namespace Infrastructure;

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
        AddMappingConfigs();

        return services;
    }

    public static void AddMappingConfigs() 
    {
        //var config = TypeAdapterConfig<Database.Models.Product, Domain.Entities.Product.Product>.NewConfig().MapToConstructor(true).Map<>
        TypeAdapterConfig<string, Domain.Entities.Product.ValueObjects.ProjectTitle>.NewConfig().MapToConstructor(true);
    }
}