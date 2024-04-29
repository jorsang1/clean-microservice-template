using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Application.Products.Dto;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Queries.GetAllProducts;

internal class GetAllProductsQueryHandler(IProductRepository productRepository)
    : IRequestHandler<GetAllProductsQuery, List<ProductListItemDto>>
{
    public async Task<List<ProductListItemDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var result = await productRepository.GetAll();
        return result.Select(p => p.Adapt<ProductListItemDto>()).ToList();
    }
}