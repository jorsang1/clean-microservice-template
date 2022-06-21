using CleanCompanyName.DDDMicroservice.Domain.Common;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products.ValueObjects;

namespace CleanCompanyName.DDDMicroservice.Domain.Entities.Products;

public sealed record Product
(
    ProductId Id,
    string Sku,
    ProductTitle Title,
    string? Description,
    decimal Price,
    DateTimeOffset CreatedOn,
    Guid CreatedBy,
    DateTimeOffset LastModifiedOn,
    Guid LastModifiedBy
)
: AuditableEntity
(
    CreatedOn,
    CreatedBy,
    LastModifiedOn,
    LastModifiedBy
)
{
    public ProductId Id { get; private set; } = Id;
    public string Sku { get; private set; } = Sku;
    public ProductTitle Title { get; private set; } = Title;
    public string? Description { get; private set; } = Description;
    public decimal Price { get; private set; } = Price;

    public Product
    (
        Guid id,
        string sku,
        string title,
        string? description,
        decimal price,
        Guid createdBy
    )
    : this
    (
        new ProductId(id),
        sku,
        new ProductTitle(title),
        description,
        price,
        CommonDateTime.Now,
        createdBy,
        CommonDateTime.Now,
        createdBy
    )
    {
    }

    public void Update
    (
        string sku,
        string title,
        string? description,
        decimal price,
        Guid modifiedBy
    )
    {
        Sku = sku;
        Title = new ProductTitle(title);
        Description = description;
        Price = price;
        base.ModifiedBy(modifiedBy);
    }
}