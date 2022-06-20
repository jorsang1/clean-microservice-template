using CleanCompanyName.DDDMicroservice.Application.Products.Dto;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Commands.UpdateProduct;

public record struct UpdateProductCommand
(
    Guid Id,
    string Sku,
    string Title,
    string? Description,
    decimal Price
)
: IRequest<ProductDto?>;