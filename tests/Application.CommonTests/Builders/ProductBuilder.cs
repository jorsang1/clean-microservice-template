using CleanCompanyName.DDDMicroservice.Domain.Common;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products.ValueObjects;

namespace CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;

public static class ProductBuilder
{
    public static Product GetProductEmpty()
    {
        return new Product
        (
            id: default,
            sku: default!,
            title: default!,
            description: default!,
            price: default,
            createdBy: default
        );
    }

    public static Product GetProduct()
    {
        return new Product
        (
            id: Guid.NewGuid(),
            sku: "sku",
            title: "title",
            description: "Description",
            price: 5,
            createdBy: Guid.NewGuid()
            
        );
    }

    public static Product GetProductWithId(Guid guid = new())
    {
        return new Product
        (
            id: guid,
            sku: default!,
            title: default!,
            description: default!,
            price: default,
            createdBy: default
        );
    }

    public static Product GetProductWithSku(string sku = "sku")
    {
        return new Product
        (
            id: default,
            sku: sku,
            title: default!,
            description: default!,
            price: default,
            createdBy: default
        );
    }

    public static Product GetProductWithTitle(string title = "Product title")
    {
        return new Product
        (
            id: default,
            sku: default!,
            title: title,
            description: default!,
            price: default,
            createdBy: default
        );
    }

    public static Product GetProductWithDescription(string description)
    {
        return new Product
        (
            id: default,
            sku: default!,
            title: default!,
            description: description,
            price: default,
            createdBy: default
        );
    }

    public static Product GetProductWithPrice(decimal price)
    {
        return new Product
        (
            id: default,
            sku: default!,
            title: default!,
            description: default!,
            price: price,
            createdBy: default
        );
    }
}