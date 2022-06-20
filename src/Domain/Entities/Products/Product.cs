using CleanCompanyName.DDDMicroservice.Domain.Common;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products.ValueObjects;

namespace CleanCompanyName.DDDMicroservice.Domain.Entities.Products;

public record Product
(
    ProductId Id,
    string Sku,
    ProductTitle Title,
    string? Description,
    decimal Price,
    Guid CreatedBy
)
: AuditableEntity
(
    CreatedBy
);