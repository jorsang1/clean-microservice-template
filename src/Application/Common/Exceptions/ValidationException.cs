using System.Text.Json;

namespace CleanCompanyName.DDDMicroservice.Application.Common.Exceptions;

public class ValidationException : Exception
{
    public List<ValidationError> Errors { get; }

    public ValidationException(List<ValidationError> errors)
        : base("Some problems has been found during validation process. Errors: " + JsonSerializer.Serialize(errors))
    {
        Errors = errors;
    }
}