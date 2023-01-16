using CleanCompanyName.DDDMicroservice.Application.Products.Dto;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Commands.UpdateProduct;

public readonly record struct UpdateProductCommand(
        Guid Id,
        string Sku,
        string Title,
        string? Description,
        decimal Price,
        Guid UserId)
    : IRequest<ProductDto?>;