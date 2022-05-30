namespace CleanCompanyName.DDDMicroservice.Infrastructure.Database.Models;

internal record struct Product(
    Guid Id,
    string Sku,
    string Title,
    string Description,
    decimal Price,
    DateTimeOffset CreatedOn,
    Guid? CreatedBy,
    DateTimeOffset LastModifiedOn,
    Guid? LastModifiedBy);