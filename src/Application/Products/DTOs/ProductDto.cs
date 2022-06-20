namespace CleanCompanyName.DDDMicroservice.Application.Products.Dto;

public record struct ProductDto
(
    Guid Id,
    string Sku,
    string Title,
    string? Description,
    decimal Price
);