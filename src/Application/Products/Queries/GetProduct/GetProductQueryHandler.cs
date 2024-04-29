using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Application.Products.Dto;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Queries.GetProduct;

internal class GetProductQueryHandler(IProductRepository productRepository)
    : IRequestHandler<GetProductQuery, ProductDto?>
{
    public async Task<ProductDto?> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var result = await productRepository.GetById(request.ProductId);

        if (result is null || result.Id.Value == Guid.Empty)
            return null;

        return result.Adapt<ProductDto>();
    }
}