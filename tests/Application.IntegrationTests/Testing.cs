using System;
using System.IO;
using System.Threading.Tasks;
using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Domain;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products;
using CleanCompanyName.DDDMicroservice.Infrastructure;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanCompanyName.DDDMicroservice.Application.IntegrationTests;

internal class Testing
{
    private static IServiceScopeFactory _scopeFactory = null!;

    public Testing()
    {
        var builder = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true);

        var configuration = builder.Build();
        
        var services = ConfigureServices(configuration);

        _scopeFactory = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>();
    }

    private ServiceCollection ConfigureServices(IConfiguration configuration)
    {
        var services = new ServiceCollection();
        
        services
            .AddApplication()
            .AddDomain()
            .AddInfrastructure(configuration.GetSection("Infrastructure"));
        
        services
            .AddLogging();
        
        return services;
    }
    
    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }
    
    public static async Task<Product?> GetById(Guid id)
    {
        using var scope = _scopeFactory.CreateScope();

        var repository = scope.ServiceProvider.GetRequiredService<IProductRepository>();

        return await repository.GetById(id);
    }

    public static async Task AddAsync(Product entity)
    {
        using var scope = _scopeFactory.CreateScope();
    
        var repository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
    
        await repository.Create(entity);
    }
}