using CleanCompanyName.DDDMicroservice.Application.Products.Dto;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Queries.GetAllProducts;

public sealed record GetAllProductsQuery : IRequest<List<ProductDto>>;