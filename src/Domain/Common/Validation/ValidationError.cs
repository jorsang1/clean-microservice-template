namespace CleanCompanyName.DDDMicroservice.Domain.Common;

public record struct ValidationError(
    string? Code,
    string Message,
    string? Tip);