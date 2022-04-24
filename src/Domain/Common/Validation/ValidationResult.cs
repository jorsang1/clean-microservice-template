namespace Domain.Common;

public record struct ValidationResult
{
    private List<ValidationError> errors = new();

    public ValidationResult()
    {
    }

    public bool IsValid => errors.Count == 0;
    public IEnumerable<ValidationError> Errors => errors.AsReadOnly();

    public void AddError(string? code, string message, string? tip)
        => AddError(new ValidationError(code, message, tip));

    public ValidationResult AddError(ValidationError error)
    {
        this.errors.Add(error);
        return this;
    }

    public ValidationResult AddErrors(IEnumerable<ValidationError> errors)
    {
        ArgumentNullException.ThrowIfNull(errors);

        this.errors.AddRange(errors);
        return this;
    }

    public ValidationResult AddErrorsFrom(IValidatable validatable)
    {
        var result = validatable.Validate();
        this.errors.AddRange(result.Errors);
        return this;
    }
}