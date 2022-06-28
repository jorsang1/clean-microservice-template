using CleanCompanyName.DDDMicroservice.Domain.Common.Validators;
using FluentAssertions;
using FluentValidation.Results;
using Xunit;

namespace Domain.UnitTests.Common.Validators;

public class ValidationResultsMapperTests
{
    [Fact]
    public void WHEN_validation_does_not_have_hint_THEN_hint_is_not_provided()
    {
        var failures = new List<ValidationFailure>
        {
            new("Product_SKU", "Must be provided", "")
            {
                ErrorCode = "Product_SKU_NotEmpty",
                CustomState = null
            }
        };

        var errors = failures.MapToValidationErrors();

        errors
            .Should()
            .ContainSingle()
            .Which
            .Hint
            .Should()
            .BeNull();
    }

    [Fact]
    public void WHEN_validation_does_not_have_hint_THEN_hint_is_provided()
    {
        var failures = new List<ValidationFailure>
        {
            new("Product_SKU", "Must be provided", "")
            {
                ErrorCode = "Product_SKU_NotEmpty",
                CustomState = new Hint("You can find information about the SKU pattern at www.skupattern.org")
            }
        };

        var errors = failures.MapToValidationErrors();

        errors
            .Should()
            .ContainSingle()
            .Which
            .Hint
            .Should()
            .NotBeNull();
    }
}