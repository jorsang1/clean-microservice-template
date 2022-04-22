using MediatR;
using Mapster;
using Application.Common.Interfaces;
using Application.Products.DTOs;

namespace Application.Products.Queries.GetAllProducts;

public class GetProductQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductListItemDto>>
{
    private IProductRepository _productRepository;
    public GetProductQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<ProductListItemDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var result = await _productRepository.GetAll();
        return result.Select(p => p.Adapt<ProductListItemDto>()).ToList();
    }
}

