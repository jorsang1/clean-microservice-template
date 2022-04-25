using MediatR;
using CleanCompanyName.DDDMicroservice.Application.Products.DTOs;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Queries.GetAllProducts;

public class GetAllProductsQuery : IRequest<List<ProductListItemDto>> 
{ 
    public Guid UserId { get; set; }
}