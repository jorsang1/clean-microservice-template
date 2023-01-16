using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;

namespace CleanCompanyName.DDDMicroservice.Infrastructure.Services;

internal class DateTimeService : IDateTimeService
{
    public DateTimeOffset Now => DateTimeOffset.UtcNow;
}