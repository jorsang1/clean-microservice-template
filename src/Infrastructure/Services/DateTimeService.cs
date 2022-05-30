using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;

namespace CleanCompanyName.DDDMicroservice.Infrastructure.Database.Services;

internal class DateTimeService : IDateTime
{
    public DateTimeOffset Now => DateTimeOffset.UtcNow;
}