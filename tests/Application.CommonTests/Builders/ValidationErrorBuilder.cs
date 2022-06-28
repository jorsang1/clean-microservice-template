using CleanCompanyName.DDDMicroservice.Domain.Common.Validators;

namespace CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;

public static class ValidationErrorBuilder
{
    public static IEnumerable<ValidationError> GetValidationErrors()
    {
        return new List<ValidationError>
        {
            new("Product_Error", "You are getting error", "Try it again!")
        };
    }
}