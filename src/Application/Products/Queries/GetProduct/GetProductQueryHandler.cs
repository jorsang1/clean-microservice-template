using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Application.Products.Dto;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Queries.GetProduct;

internal class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDto?>
{
    private readonly IProductRepository _productRepository;

    public GetProductQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto?> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var result = await _productRepository.GetById(request.ProductId);

        if (result is null || result.Id.Value == Guid.Empty)
            return null;

        return result.Adapt<ProductDto>();
    }
}