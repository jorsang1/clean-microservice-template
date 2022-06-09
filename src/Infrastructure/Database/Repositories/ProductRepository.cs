using Mapster;
using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Infrastructure.Database.Models;

namespace CleanCompanyName.DDDMicroservice.Infrastructure.Database.Repositories;

internal class ProductRepository : IProductRepository
{
    private static readonly List<Product> Products = new()
    {
        new Product(Guid.NewGuid(), "SKU code", "The title", "Prod description", 23.5M, DateTime.Now, Guid.NewGuid(), DateTime.Now, Guid.NewGuid())
    };

    public async Task<Domain.Entities.Products.Product?> GetById(Guid id)
    {
        var product = Products.FirstOrDefault(p => p.Id == id);

        if (product == default)
            return null;

        return product
            .Adapt<Domain.Entities.Products.Product>();
    }

    public async Task<List<Domain.Entities.Products.Product>> GetAll()
    {
        return Products.Select(p => p.Adapt<Domain.Entities.Products.Product>()).ToList();
    }

    public async Task<Domain.Entities.Products.Product> Create(Domain.Entities.Products.Product product)
    {
        Products.Add(product.Adapt<Product>());
        return Products.Last().Adapt<Domain.Entities.Products.Product>();
    }

    public async Task Update(Domain.Entities.Products.Product product)
    {
        Products.Remove(Products.First(p => p.Id == product.Id));
        Products.Add(product.Adapt<Product>());
    }

    public async Task Delete(Domain.Entities.Products.Product product)
    {
        Products.Remove(Products.First(p => p.Id == product.Id));
    }

    public void Clear()
    {
        Products.Clear();
    }

    public async Task<int> CountAsync()
    {
        return Products.Count;
    }
}