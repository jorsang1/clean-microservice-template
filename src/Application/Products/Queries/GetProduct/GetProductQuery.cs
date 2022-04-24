using MediatR;
using Application.Products.DTOs;

namespace Application.Products.Queries.GetProduct;

public class GetProductQuery : IRequest<ProductDto>
{
    public Guid ProductId { get; set; }
};
