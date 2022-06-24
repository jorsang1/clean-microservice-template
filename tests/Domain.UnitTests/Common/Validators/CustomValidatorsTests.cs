using CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;
using CleanCompanyName.DDDMicroservice.Domain.Common.Validators;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products;
using FluentAssertions;
using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace CleanCompanyName.DDDMicroservice.Domain.UnitTests.Common.Validators;

public class CustomValidatorsTests
{
    private readonly InlineValidator<Product> _validator;

    public CustomValidatorsTests()
    {
        _validator = new InlineValidator<Product>();
    }

    [Fact]
    public void WHEN_AutomaticErrorCode_is_well_setup_THEN_validator_return_well_formed_ErrorCode()
    {
        _validator
            .RuleFor(product => product.Sku)
            .NotEmpty()
            .WithAutomaticErrorCode(_validator);

        var result = _validator.TestValidate(ProductBuilder.GetProductWithSku(string.Empty));

        result.Errors
            .Should()
            .ContainSingle()
            .Which
            .ErrorCode
            .Should()
            .Be("Product_Sku_NotEmpty");
    }
    
    [Fact]
    public void WHEN_AutomaticErrorCode_is_not_well_setup_THEN_validator_return_partial_ErrorCode()
    {
        _validator
            .RuleFor(product => product.Sku)
            .NotEmpty()
            .WithAutomaticErrorCode(new InlineValidator<Product>());

        var result = _validator.TestValidate(ProductBuilder.GetProductWithSku(string.Empty));

        result.Errors
            .Should()
            .ContainSingle()
            .Which
            .ErrorCode
            .Should()
            .Be("Product");
    }
    
    [Fact]
    public void WHEN_hint_is_well_setup_THEN_validator_return_well_formed_Hint()
    {
        var hint = "You can find information about the SKU pattern at www.skupattern.org";
        _validator
            .RuleFor(product => product.Sku)
            .NotEmpty()
            .WithHint(hint);

        var result = _validator.TestValidate(ProductBuilder.GetProductWithSku(string.Empty));

        result.Errors
            .MapToValidationErrors()
            .Should()
            .ContainSingle()
            .Which
            .Hint
            .Should().Be(hint);
    }
}