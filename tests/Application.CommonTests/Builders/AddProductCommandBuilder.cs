﻿using CleanCompanyName.DDDMicroservice.Application.Products.Commands.AddProduct;

namespace CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;

public static class AddProductCommandBuilder
{
    public static AddProductCommand GetAddProductCommandEmpty()
    {
        return new AddProductCommand
        (
            Id: default,
            Sku: default!,
            Title: default!,
            Description: default!,
            Price: default
        );
    }
}