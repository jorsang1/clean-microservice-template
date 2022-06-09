using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CleanCompanyName.DDDMicroservice.Application.IntegrationTests;

// [Collection("Sequential")]
public class TestBase : IClassFixture<Testing>
{
    private readonly IServiceScopeFactory _scopeFactory;

    protected TestBase(Testing testing)
    {
        _scopeFactory = testing.ScopeFactory;
    }

    protected async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    protected async Task<Product?> GetById(Guid id)
    {
        using var scope = _scopeFactory.CreateScope();

        var repository = scope.ServiceProvider.GetRequiredService<IProductRepository>();

        return await repository.GetById(id);
    }

    protected async Task AddAsync(Product entity)
    {
        using var scope = _scopeFactory.CreateScope();

        var repository = scope.ServiceProvider.GetRequiredService<IProductRepository>();

        await repository.Create(entity);
    }

    public void Clear()
    {
        using var scope = _scopeFactory.CreateScope();

        var repository = scope.ServiceProvider.GetRequiredService<IProductRepository>();

        repository.Clear();
    }

    protected async Task<int> CountAsync()
    {
        using var scope = _scopeFactory.CreateScope();

        var repository = scope.ServiceProvider.GetRequiredService<IProductRepository>();

        return await repository.CountAsync();
    }

}