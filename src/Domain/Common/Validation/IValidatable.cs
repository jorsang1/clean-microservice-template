namespace Domain.Common;

public interface IValidatable
{
    ValidationResult Validate();
}