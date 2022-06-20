using CleanCompanyName.DDDMicroservice.Domain.Entities.Products;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products.ValueObjects;

namespace CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;

public static class ProductBuilder
{
    public static Product GetProductEmpty()
    {
        return new Product
        (
            Id: default,
            Sku: default!,
            Title: default,
            Description: default!,
            Price: default,
            CreatedBy: default
        );
    }

    public static Product GetProduct()
    {
        return new Product
        (
            Id: new ProductId(Guid.NewGuid()),
            Sku: "sku",
            Title: new ProductTitle("title"),
            Description: "Description",
            Price: 5,
            CreatedBy: Guid.NewGuid()
        );
    }

    public static Product GetProductWithId(Guid guid = new())
    {
        return GetProductEmpty() with
        {
            Id = new ProductId(guid)
        };
    }

    public static Product GetProductWithSku(string sku = "sku")
    {
        return GetProductEmpty() with
        {
            Sku = sku
        };
    }

    public static Product GetProductWithTitle(string title = "Product title")
    {
        return GetProductEmpty() with
        {
            Title = new ProductTitle(title)
        };
    }

    public static Product GetProductWithDescription(string description)
    {
        return GetProductEmpty() with
        {
            Description = description
        };
    }

    public static Product GetProductWithPrice(decimal price)
    {
        return GetProductEmpty() with
        {
            Price = price
        };
    }
}