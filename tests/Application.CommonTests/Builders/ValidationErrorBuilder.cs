using CleanCompanyName.DDDMicroservice.Domain.Common.Validators;

namespace CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;

public interface IValidationErrorSelectionStage
{
    IValidationErrorGetterStage WithDefaultErrorMessage();
    IValidationErrorGetterStage WithCustomErrorMessage(string message);
}

public interface IValidationErrorGetterStage
{
    List<ValidationError> Get();
}

public class ValidationErrorBuilder :
    IValidationErrorSelectionStage,
    IValidationErrorGetterStage
{
    private List<ValidationError> _validationErrors;

    private ValidationErrorBuilder() {}

    public static IValidationErrorSelectionStage Init()
    {
        return new ValidationErrorBuilder();
    }

    public IValidationErrorGetterStage WithDefaultErrorMessage()
    {
        _validationErrors = new List<ValidationError>
        {
            new("Product_Error", "You are getting error", "Try it again!")
        };

        return this;
    }

    public IValidationErrorGetterStage WithCustomErrorMessage(string message)
    {
        _validationErrors = new List<ValidationError>
        {
            new("Product_Error", message, "Try it again!")
        };

        return this;
    }

    public List<ValidationError> Get()
    {
        return _validationErrors;
    }
}