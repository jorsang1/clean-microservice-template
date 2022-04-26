﻿using Mapster;
using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Application.Products.DTOs;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Queries.GetAllProducts;

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