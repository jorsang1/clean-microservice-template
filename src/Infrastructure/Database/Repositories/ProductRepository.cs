using Mapster;
using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Infrastructure.Database.Models;

namespace CleanCompanyName.DDDMicroservice.Infrastructure.Database.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly static List<Product> Products = new()
    {
        new Product(Guid.NewGuid(), "SKU code", "The title", "Prod description", 23.5M, DateTime.Now, Guid.NewGuid(), DateTime.Now, Guid.NewGuid())
    };

    public ProductRepository()
    {
    }

    public async Task<Domain.Entities.Product.Product> GetById(Guid id)
    {
        return Products.FirstOrDefault(p => p.Id == id)!.Adapt<Domain.Entities.Product.Product>();
    }

    public async Task<List<Domain.Entities.Product.Product>> GetAll()
    {
        return Products.Select(p => p.Adapt<Domain.Entities.Product.Product>()).ToList();
    }

    public async Task<Domain.Entities.Product.Product> Create(Domain.Entities.Product.Product product)
    {
        Products.Add(product.Adapt<Product>());
        return Products.Last().Adapt<Domain.Entities.Product.Product>();//.MapToEntity();
    }

    public async Task Update(Domain.Entities.Product.Product product)
    {
        Products.Remove(Products.First(p => p.Id == product.Id));
        Products.Add(product.Adapt<Product>());
    }

    public async Task Delete(Domain.Entities.Product.Product product)
    {
        Products.Remove(Products.First(p => p.Id == product.Id));
    }
}