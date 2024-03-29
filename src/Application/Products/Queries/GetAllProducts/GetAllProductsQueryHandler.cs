﻿using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Application.Products.Dto;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Queries.GetAllProducts;

internal class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductListItemDto>>
{
    private readonly IProductRepository _productRepository;

    public GetAllProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<ProductListItemDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var result = await _productRepository.GetAll();
        return result.Select(p => p.Adapt<ProductListItemDto>()).ToList();
    }
}