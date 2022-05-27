using CleanCompanyName.DDDMicroservice.Domain.Common.Validators;

namespace CleanCompanyName.DDDMicroservice.Application.Common.Exceptions;

public class ValidationException : Exception
{
    public IEnumerable<ValidationError> Errors { get; }

    public ValidationException(IEnumerable<ValidationError> errors)
        : base("Some problems has been found during validation process:")
    {
        Errors = errors;
    }
}