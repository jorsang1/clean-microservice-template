namespace CleanCompanyName.DDDMicroservice.Application.Products.Dto;

public sealed record ProductDto
(
    Guid Id,
    string Sku,
    string Title,
    string? Description,
    decimal Price
);