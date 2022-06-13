using CleanCompanyName.DDDMicroservice.Application.Products.Commands.AddProduct;
using CleanCompanyName.DDDMicroservice.Domain.Common.Validators;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products.ValueObjects;

namespace CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;

public static class ProductBuilder
{
    public static Product GetProductEmpty()
    {
        return new Product();
    }

    public static Product GetProduct()
    {
        return new Product
        {
            Id = Guid.NewGuid(),
            Sku = "sku",
            Title = new ProjectTitle("title"),
            Description = "Description",
            Price = 5
        };
    }

    public static Product GetProductWithSku()
    {
        return new Product
        {
            Sku = "sku",
        };
    }
}