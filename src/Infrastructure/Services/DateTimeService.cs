using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;

namespace CleanCompanyName.DDDMicroservice.Infrastructure.Database.Services;

public class DateTimeService : IDateTime
{
    public DateTimeOffset Now => DateTimeOffset.UtcNow;
}