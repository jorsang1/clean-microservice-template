using CleanCompanyName.DDDMicroservice.Domain.Entities.Products.ValueObjects;
using FluentValidation.TestHelper;
using Xunit;

namespace CleanCompanyName.DDDMicroservice.Domain.UnitTests.Entities.Products.ValueObjects;

public class ProductTitleValidatorTests
{
    private readonly ProductTitleValidator _validator;

    public ProductTitleValidatorTests()
    {
        _validator = new ProductTitleValidator();
    }
    
    [Fact]
    public void WHEN_Title_is_empty_THEN_give_an_error()
    {
        var model = new ProductTitle { Value = string.Empty };
        var result = _validator.TestValidate(model);

        result
            .ShouldHaveValidationErrorFor(person => person.Value);
    }

    [Fact]
    public void WHEN_Title_is_specified_THEN_not_give_an_error()
    {
        var model = new ProductTitle { Value = "some title" };
        var result = _validator.TestValidate(model);

        result
            .ShouldNotHaveValidationErrorFor(person => person.Value);
    }

    [Fact]
    public void WHEN_Title_is_below_5_characters_THEN_give_an_error()
    {
        var model = new ProductTitle { Value = "some" };
        var result = _validator.TestValidate(model);

        result
            .ShouldHaveValidationErrorFor(person => person.Value);
    } 
    
    [Fact]
    public void WHEN_Title_is_above_350_characters_THEN_give_an_error()
    {
        var model = new ProductTitle
        {
            Value = string.Join
            (
                "", 
                Enumerable.Repeat("some", 2000000)
            )
        };
        var result = _validator.TestValidate(model);

        result
            .ShouldHaveValidationErrorFor(person => person.Value);
    }
}