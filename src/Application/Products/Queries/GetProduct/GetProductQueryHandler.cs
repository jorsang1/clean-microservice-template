using MediatR;
using Mapster;
using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Application.Products.DTOs;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Queries.GetProduct;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDto>
{
    private IProductRepository _productRepository;

    public GetProductQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var result = await _productRepository.GetById(request.ProductId);
        return result.Adapt<ProductDto>();
    }
}