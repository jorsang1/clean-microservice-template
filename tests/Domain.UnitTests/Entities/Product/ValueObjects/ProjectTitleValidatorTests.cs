using System.Linq;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products.ValueObjects;
using FluentValidation.TestHelper;
using Xunit;

namespace CleanCompanyName.DDDMicroservice.Domain.UnitTests.Entities.Products.ValueObjects;

public class ProjectTitleValidatorTests
{
    private readonly ProjectTitleValidator _validator;

    public ProjectTitleValidatorTests()
    {
        _validator = new ProjectTitleValidator();
    }
    
    [Fact]
    public void WHEN_Title_is_empty_THEN_give_an_error()
    {
        var model = new ProjectTitle { Title = string.Empty };
        var result = _validator.TestValidate(model);

        result
            .ShouldHaveValidationErrorFor(person => person.Title);
    }

    [Fact]
    public void WHEN_Title_is_specified_THEN_not_give_an_error()
    {
        var model = new ProjectTitle { Title = "some title" };
        var result = _validator.TestValidate(model);

        result
            .ShouldNotHaveValidationErrorFor(person => person.Title);
    }

    [Fact]
    public void WHEN_Title_is_below_5_characters_THEN_give_an_error()
    {
        var model = new ProjectTitle { Title = "some" };
        var result = _validator.TestValidate(model);

        result
            .ShouldHaveValidationErrorFor(person => person.Title);
    } 
    
    [Fact]
    public void WHEN_Title_is_above_350_characters_THEN_give_an_error()
    {
        var model = new ProjectTitle
        {
            Title = string.Join
            (
                "", 
                Enumerable.Repeat("some", 2000000)
            )
        };
        var result = _validator.TestValidate(model);

        result
            .ShouldHaveValidationErrorFor(person => person.Title);
    }
}