namespace CleanCompanyName.DDDMicroservice.Domain.Common.Validators;

public record ValidationError(
    string Code,
    string Message,
    string? Hint);