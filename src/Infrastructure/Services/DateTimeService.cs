using Application.Common.Interfaces;

namespace Infrastructure.Database.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
