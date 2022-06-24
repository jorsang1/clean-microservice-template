using CleanCompanyName.DDDMicroservice.Application.Products.Dto;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Queries.GetProduct;

public sealed record GetProductQuery(Guid ProductId) : IRequest<ProductDto?>;