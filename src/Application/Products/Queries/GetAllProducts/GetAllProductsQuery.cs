using CleanCompanyName.DDDMicroservice.Application.Products.Dto;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Queries.GetAllProducts;

public readonly record struct GetAllProductsQuery : IRequest<List<ProductListItemDto>>;