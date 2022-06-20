﻿using CleanCompanyName.DDDMicroservice.Application.Products.Dto;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Queries.GetAllProducts;

public record struct GetAllProductsQuery : IRequest<List<ProductDto>>;