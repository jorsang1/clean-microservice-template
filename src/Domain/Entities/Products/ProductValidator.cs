﻿using CleanCompanyName.DDDMicroservice.Domain.Common.Validators;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products.ValueObjects;
using FluentValidation;

namespace CleanCompanyName.DDDMicroservice.Domain.Entities.Products;

internal class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(product => product.Id.Value)
            .NotEmpty();

        RuleFor(product => product.Sku)
            .NotEmpty()
                .WithAutomaticErrorCode(this)
                .WithHint("You can find information about the SKU pattern at www.skupattern.org");

        RuleFor(product => product.Title)
            .SetValidator(new ProductTitleValidator());

        RuleFor(product => product.Description)
            .NotEmpty()
                .WithAutomaticErrorCode(this);

        RuleFor(product => product.Price)
            .GreaterThanOrEqualTo(0)
                .WithAutomaticErrorCode(this);
    }
}
