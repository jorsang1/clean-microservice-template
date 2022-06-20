using CleanCompanyName.DDDMicroservice.Application.Products.Dto;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Queries.GetProduct;

public record struct GetProductQuery(Guid ProductId) : IRequest<ProductDto?>;