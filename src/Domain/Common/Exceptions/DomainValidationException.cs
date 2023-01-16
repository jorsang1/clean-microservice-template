using CleanCompanyName.DDDMicroservice.Domain.Common.Validators;

namespace CleanCompanyName.DDDMicroservice.Domain.Common.Exceptions;

public class DomainValidationException : Exception
{
    public IEnumerable<ValidationError> Errors { get; }

    public DomainValidationException(IEnumerable<ValidationError> errors)
        : base("Some problems has been found during validation process:")
    {
        Errors = errors;
    }
}