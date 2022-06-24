using CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products;
using FluentValidation.TestHelper;
using Xunit;

namespace CleanCompanyName.DDDMicroservice.Domain.UnitTests.Entities.Products;

public class ProductValidatorTests
{
    private readonly ProductValidator _validator;

    public ProductValidatorTests()
    {
        _validator = new ProductValidator();
    }

    [Fact]
    public void WHEN_Id_is_empty_THEN_gives_an_error()
    {
        var sut = ProductBuilder.GetProductWithId(Guid.Empty);
        var result = _validator.TestValidate(sut);

        result
            .ShouldHaveValidationErrorFor(person => person.Id.Value);
    }

    [Fact]
    public void WHEN_Sku_is_empty_THEN_gives_an_error()
    {
        var sut = ProductBuilder.GetProductWithSku(string.Empty);
        var result = _validator.TestValidate(sut);

        result
            .ShouldHaveValidationErrorFor(product => product.Sku);
    }

    [Fact]
    public void WHEN_Sku_is_specified_THEN_does_not_give_an_error()
    {
        var sut = ProductBuilder.GetProductWithSku("some sku");
        var result = _validator.TestValidate(sut);

        result
            .ShouldNotHaveValidationErrorFor(product => product.Sku);
    }

    [Fact]
    public void WHEN_Title_is_empty_THEN_gives_an_error()
    {
        var sut = ProductBuilder.GetProductWithTitle(string.Empty);
        var result = _validator.TestValidate(sut);

        result
            .ShouldHaveValidationErrorFor(product => product.Title.Value);
    }

 [Fact]
    public void WHEN_Description_is_empty_THEN_gives_an_error()
    {
        var sut = ProductBuilder.GetProductWithDescription(string.Empty);
        var result = _validator.TestValidate(sut);

        result
            .ShouldHaveValidationErrorFor(product => product.Description);
    }

    [Fact]
    public void WHEN_Price_is_below_zero_THEN_gives_an_error()
    {
        var sut = ProductBuilder.GetProductWithPrice(-1);
        var result = _validator.TestValidate(sut);

        result
            .ShouldHaveValidationErrorFor(product => product.Price);
    }

    [Fact]
    public void WHEN_Price_is_above_zero_THEN_does_not_give_an_error()
    {
        var sut = ProductBuilder.GetProductWithPrice(5);
        var result = _validator.TestValidate(sut);

        result
            .ShouldNotHaveValidationErrorFor(product => product.Price);
    }
}