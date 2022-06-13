using CleanCompanyName.DDDMicroservice.Domain.Entities.Products;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products.ValueObjects;
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
    public void WHEN_Id_is_empty_THEN_give_an_error()
    {
        var model = new Product { Id = new ProductId(Guid.Empty) };
        var result = _validator.TestValidate(model);

        result
            .ShouldHaveValidationErrorFor(person => person.Id.Value);
    }

    [Fact]
    public void WHEN_Sku_is_empty_THEN_give_an_error()
    {
        var model = new Product { Sku = string.Empty };
        var result = _validator.TestValidate(model);

        result
            .ShouldHaveValidationErrorFor(product => product.Sku);
    }

    [Fact]
    public void WHEN_Sku_is_specified_THEN_not_give_an_error()
    {
        var model = new Product { Sku = "some sku" };
        var result = _validator.TestValidate(model);

        result
            .ShouldNotHaveValidationErrorFor(product => product.Sku);
    }

    [Fact]
    public void WHEN_Title_is_empty_THEN_give_an_error()
    {
        var model = new Product();
        var result = _validator.TestValidate(model);

        result
            .ShouldHaveValidationErrorFor(product => product.Title.Value);
    }

 [Fact]
    public void WHEN_Description_is_empty_THEN_give_an_error()
    {
        var model = new Product();
        var result = _validator.TestValidate(model);

        result
            .ShouldHaveValidationErrorFor(product => product.Description);
    }

    [Fact]
    public void WHEN_Price_is_below_zero_THEN_give_an_error()
    {
        var model = new Product { Price = -1 };
        var result = _validator.TestValidate(model);

        result
            .ShouldHaveValidationErrorFor(product => product.Price);
    }

    [Fact]
    public void WHEN_Price_is_above_zero_THEN_not_give_an_error()
    {
        var model = new Product { Price = 5 };
        var result = _validator.TestValidate(model);

        result
            .ShouldNotHaveValidationErrorFor(product => product.Price);
    }
}