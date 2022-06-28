using FluentValidation.Results;

namespace CleanCompanyName.DDDMicroservice.Domain.Common.Validators;

public static class ValidationResultsMapper
{
    public static List<ValidationError> MapToValidationErrors(this List<ValidationFailure> failures)
    {
        return failures
            .Select(e =>
                new ValidationError(
                    e.ErrorCode,
                    e.ErrorMessage,
                    ((Hint?)e.CustomState)?.Message))
            .ToList();
    }
}