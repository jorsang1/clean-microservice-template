using CleanCompanyName.DDDMicroservice.Application.Products.DTOs;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Queries.GetProduct;

public class GetProductQuery : IRequest<ProductDto>
{
    public Guid ProductId { get; set; }
}