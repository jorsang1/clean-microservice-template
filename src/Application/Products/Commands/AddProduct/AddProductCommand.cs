using CleanCompanyName.DDDMicroservice.Application.Products.Dto;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Commands.AddProduct;

public sealed record AddProductCommand
(
    Guid Id,
    string Sku,
    string Title,
    string? Description,
    decimal Price
)
: IRequest<ProductDto>;