using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;

namespace CleanCompanyName.DDDMicroservice.Infrastructure.Services;

internal class DateTimeService : IDateTime
{
    public DateTimeOffset Now => DateTimeOffset.UtcNow;
}