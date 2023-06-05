using CleanCompanyName.DDDMicroservice.Application.Products.Dto;
using FluentResults;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Commands.UpdateProduct;

public readonly record struct UpdateProductCommand(
        Guid Id,
        string Sku,
        string Title,
        string? Description,
        decimal Price,
        Guid UserId)
    : IRequest<Result<ProductDto>>;