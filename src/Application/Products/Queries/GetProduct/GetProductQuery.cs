using CleanCompanyName.DDDMicroservice.Application.Products.Dto;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Queries.GetProduct;

public record struct GetProductQuery : IRequest<ProductDto?>
{
    public Guid ProductId { get; set; }
}