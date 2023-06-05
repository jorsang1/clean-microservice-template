using CleanCompanyName.DDDMicroservice.Domain.Common.Validators;
using FluentResults;

namespace CleanCompanyName.DDDMicroservice.Application.Common.Mappers;

public static class ValidationErrorsMapper
{
    public static List<Error> MapToFluentErrors(this List<ValidationError> errors)
    {
        return errors
            .Select(e =>
                new Error(e.Message)
                {
                    Metadata = { {"Code", e.Code}, {"Hint", e.Hint}, {"Status", ResultStatus.Invalid} }
                })
            .ToList();
    }
}