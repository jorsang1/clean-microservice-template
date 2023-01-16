namespace CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
public interface IDateTimeService
{
    public DateTimeOffset Now { get; }
}