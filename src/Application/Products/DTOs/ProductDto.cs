namespace CleanCompanyName.DDDMicroservice.Application.Products.Dto;

public readonly record struct ProductDto
(
    Guid Id,
    string Sku,
    string Title,
    string? Description,
    decimal Price
);