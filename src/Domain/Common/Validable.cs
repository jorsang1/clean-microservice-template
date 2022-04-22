namespace Domain.Common;

public abstract class Validable
{
    private bool _hasBeenValidated = false; //TODO: Shall we control this so we don't ask twice?
    public List<ValidationError> Errors { get; set; }

    protected Validable()
    {
        Errors = new List<ValidationError>();
    }

    public void AddError(string? code, string message, string? tip)
        => Errors.Add(new ValidationError { Code = code, Message = message, Tip = tip });

    public void AddErrors(List<ValidationError> errors)
        => Errors.AddRange(errors);

    public bool IsValid()
    {
        Validate();
        return !Errors.Any();
    }

    protected abstract void Validate();
}
