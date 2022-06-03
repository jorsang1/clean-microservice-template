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
    public void WHEN_Id_is_empty_THEN_give_an_error()
    {
        var model = new Product { Id = Guid.Empty };
        var result = _validator.TestValidate(model);

        result
            .ShouldHaveValidationErrorFor(person => person.Id);
    }

    [Fact]
    public void WHEN_Sku_is_empty_THEN_give_an_error()
    {
        var model = new Product { Sku = string.Empty };
        var result = _validator.TestValidate(model);

        result
            .ShouldHaveValidationErrorFor(person => person.Sku);
    }

    [Fact]
    public void WHEN_Sku_is_specified_THEN_not_give_an_error()
    {
        var model = new Product { Sku = "some sku" };
        var result = _validator.TestValidate(model);

        result
            .ShouldNotHaveValidationErrorFor(person => person.Sku);
    }

    [Fact]
    public void WHEN_Title_is_empty_THEN_give_an_error()
    {
        var model = new Product();
        var result = _validator.TestValidate(model);

        result
            .ShouldHaveValidationErrorFor(person => person.Title.Title);
    }

 [Fact]
    public void WHEN_Description_is_empty_THEN_give_an_error()
    {
        var model = new Product();
        var result = _validator.TestValidate(model);

        result
            .ShouldHaveValidationErrorFor(person => person.Description);
    }

    [Fact]
    public void WHEN_Price_is_below_zero_THEN_give_an_error()
    {
        var model = new Product { Price = -1 };
        var result = _validator.TestValidate(model);

        result
            .ShouldHaveValidationErrorFor(person => person.Price);
    }

    [Fact]
    public void WHEN_Price_is_above_zero_THEN_not_give_an_error()
    {
        var model = new Product { Price = 5 };
        var result = _validator.TestValidate(model);

        result
            .ShouldNotHaveValidationErrorFor(person => person.Price);
    }
}