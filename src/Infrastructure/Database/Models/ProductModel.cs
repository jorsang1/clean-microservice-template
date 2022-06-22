namespace CleanCompanyName.DDDMicroservice.Infrastructure.Database.Models;

internal sealed record ProductModel(
    Guid Id,
    string Sku,
    string Title,
    string? Description,
    decimal Price,
    DateTimeOffset CreatedOn,
    Guid CreatedBy,
    DateTimeOffset LastModifiedOn,
    Guid LastModifiedBy);