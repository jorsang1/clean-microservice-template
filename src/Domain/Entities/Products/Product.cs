using CleanCompanyName.DDDMicroservice.Domain.Common;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products.ValueObjects;

namespace CleanCompanyName.DDDMicroservice.Domain.Entities.Products;

public class Product : AuditableEntity
{
    public ProductId Id { get; init; }
    public string Sku { get; init; } = default!;
    public ProductTitle Title { get; init; }
    public string Description { get; init; } = default!;
    public decimal Price { get; init; }
}