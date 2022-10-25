﻿using CleanCompanyName.DDDMicroservice.Application.Products.Dto;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Commands.AddProduct;

public readonly record struct AddProductCommand(
        Guid Id,
        string Sku,
        string Title,
        string? Description,
        decimal Price,
        Guid UserId)
    : IRequest<ProductDto>;