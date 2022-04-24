using MediatR;
using Application.Products.DTOs;

namespace Application.Products.Queries.GetAllProducts;

public class GetAllProductsQuery : IRequest<List<ProductListItemDto>> 
{ 
    public Guid UserId { get; set; }
};
