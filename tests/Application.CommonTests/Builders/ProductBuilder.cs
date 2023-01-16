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
            Title: default!,
            Description: default!,
            Price: default,
            CreatedOn: default,
            CreatedBy: default,
            LastModifiedOn: default,
            LastModifiedBy: default
        );
    }

    public static Product GetProduct()
    {
        var createdBy = Guid.NewGuid();
        var createdOn = DateTimeOffset.Now;

        return new Product
        (
            Id: new ProductId(Guid.NewGuid()),
            Sku: "sku",
            Title: new ProductTitle("title"),
            Description: "Description",
            Price: 5,
            CreatedOn: createdOn,
            CreatedBy: createdBy,
            LastModifiedOn: createdOn,
            LastModifiedBy: createdBy
        );
    }

    public static Product GetProductWithId(Guid guid = new())
    {
        return new Product
        (
            Id: new ProductId(guid),
            Sku: default!,
            Title: default!,
            Description: default!,
            Price: default,
            CreatedOn: default,
            CreatedBy: default,
            LastModifiedOn: default,
            LastModifiedBy: default
        );
    }

    public static Product GetProductWithSku(string sku = "sku")
    {
        return new Product
        (
            Id: default,
            Sku: sku,
            Title: default!,
            Description: default!,
            Price: default,
            CreatedOn: default,
            CreatedBy: default,
            LastModifiedOn: default,
            LastModifiedBy: default
        );
    }

    public static Product GetProductWithTitle(string title = "Product title")
    {
        return new Product
        (
            Id: default,
            Sku: default!,
            Title: new ProductTitle(title),
            Description: default!,
            Price: default,
            CreatedOn: default,
            CreatedBy: default,
            LastModifiedOn: default,
            LastModifiedBy: default
        );
    }

    public static Product GetProductWithDescription(string description)
    {
        return new Product
        (
            Id: default,
            Sku: default!,
            Title: default!,
            Description: description,
            Price: default,
            CreatedOn: default,
            CreatedBy: default,
            LastModifiedOn: default,
            LastModifiedBy: default
        );
    }

    public static Product GetProductWithPrice(decimal price)
    {
        return new Product
        (
            Id: default,
            Sku: default!,
            Title: default!,
            Description: default!,
            Price: price,
            CreatedOn: default,
            CreatedBy: default,
            LastModifiedOn: default,
            LastModifiedBy: default
        );
    }
}