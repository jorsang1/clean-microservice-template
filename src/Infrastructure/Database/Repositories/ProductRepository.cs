using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Infrastructure.Database.Mappers;
using CleanCompanyName.DDDMicroservice.Infrastructure.Database.Models;
using Mapster;

namespace CleanCompanyName.DDDMicroservice.Infrastructure.Database.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly static List<Product> Products = new()
    {
        new Product(Guid.NewGuid(), "SKU code", "Title", "Prod description", 23.5M, DateTime.Now, "admin", DateTime.Now, "admin")
    };

    public ProductRepository()
    {
    }

    public async Task<Domain.Entities.Product.Product> GetById(Guid id)
    {
        return Products.FirstOrDefault(p => p.Id == id)!.MapToEntity();//.Adapt<Domain.Entities.Product.Product>();
    }

    public async Task<List<Domain.Entities.Product.Product>> GetAll()
    {
        //var config = new TypeAdapterConfig();
        //config.NewConfig<Database.Models.Product, Domain.Entities.Product.Product>()
        //    .Map(d => d.Title, s => new Domain.Entities.Product.ValueObjects.ProjectTitle(s.Title));

        //return Products.Select(p => p.Adapt<Domain.Entities.Product.Product>(config)).ToList();
        return Products.Select(p => p.MapToEntity()).ToList();
    }

    public async Task<Domain.Entities.Product.Product> Create(Domain.Entities.Product.Product product)
    {
        Products.Add(product.Adapt<Product>());
        return Products.Last().MapToEntity();
    }

    public async Task Update(Domain.Entities.Product.Product product)
    {
        Products.Remove(Products.First(p => p.Id == product.Id));
        Products.Add(product.Adapt<Product>());
    }

    public async Task Delete(Domain.Entities.Product.Product product)
    {
        Products.Remove(product.Adapt<Product>());
    }
}