using Mapster;
using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products;
using CleanCompanyName.DDDMicroservice.Infrastructure.Database.Models;

namespace CleanCompanyName.DDDMicroservice.Infrastructure.Database.Repositories;

internal class ProductRepository : IProductRepository
{
    private static readonly List<ProductModel> Products = new()
    {
        new ProductModel(Guid.NewGuid(), "SKU code", "The title", "Prod description", 23.5M, DateTime.Now, Guid.NewGuid(), DateTime.Now, Guid.NewGuid())
    };

    public async Task<Product?> GetById(Guid id)
    {
        var product = Products.FirstOrDefault(p => p.Id == id);

        return product?.Adapt<Product>();
    }

    public async Task<List<Product>> GetAll()
    {
        return Products.Select(p => p.Adapt<Product>()).ToList();
    }

    public async Task<Product> Create(Product product)
    {
        Products.Add(product.Adapt<ProductModel>());
        return Products.Last().Adapt<Product>();
    }

    public async Task Update(Product product)
    {
        Products.Remove(Products.First(p => p.Id == product.Id.Value));
        Products.Add(product.Adapt<ProductModel>());
    }

    public async Task Delete(Product product)
    {
        Products.Remove(Products.First(p => p.Id == product.Id.Value));
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