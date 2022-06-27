namespace CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
public interface IDateTime
{
    public DateTimeOffset Now { get; }
}