namespace Application.Common.Exceptions;

public struct ValidationError
{
    public string? Code { get; set; }
    public string Message { get; set; }
    public string? Tip { get; set; }
}