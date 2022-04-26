namespace CleanCompanyName.DDDMicroservice.Domain.Common;

public interface IValidatable
{
    ValidationResult Validate();
}