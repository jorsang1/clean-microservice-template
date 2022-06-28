using CleanCompanyName.DDDMicroservice.Application.Products.Dto;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Queries.GetProduct;

public readonly record struct GetProductQuery(Guid ProductId) : IRequest<ProductDto?>;