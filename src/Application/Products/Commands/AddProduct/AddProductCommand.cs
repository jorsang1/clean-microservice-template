using CleanCompanyName.DDDMicroservice.Application.Products.Dto;
using FluentResults;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Commands.AddProduct;

public readonly record struct AddProductCommand(
        Guid Id,
        string Sku,
        string Title,
        string? Description,
        decimal Price,
        Guid UserId)
    : IRequest<Result<ProductDto>>;