using CleanCompanyName.DDDMicroservice.Domain.Common;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Product.ValueObjects;

namespace CleanCompanyName.DDDMicroservice.Domain.Entities.Product;

public class Product : AuditableEntity
{
    public Guid Id { get; init; }
    public string Sku { get; init; }
    public ProjectTitle Title { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
}