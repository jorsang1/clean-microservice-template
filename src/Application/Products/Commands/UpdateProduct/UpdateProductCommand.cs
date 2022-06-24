using CleanCompanyName.DDDMicroservice.Application.Products.Dto;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Commands.UpdateProduct;

public sealed record UpdateProductCommand(
        Guid Id,
        string Sku,
        string Title,
        string? Description,
        decimal Price)
    : IRequest<ProductDto?>;