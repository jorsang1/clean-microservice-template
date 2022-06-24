using CleanCompanyName.DDDMicroservice.Domain.Entities.Products;

namespace CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;

public static class ProductBuilder
{
    public static Product GetProductEmpty()
    {
        return Product.Create
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
        return Product.Create
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
        return Product.Create
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
        return Product.Create
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
        return Product.Create
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
        return Product.Create
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
        return Product.Create
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