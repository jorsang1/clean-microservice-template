using CleanCompanyName.DDDMicroservice.Infrastructure.Database.Services;

namespace CleanCompanyName.DDDMicroservice.Infrastructure.UnitTests.Services;

public class DateTimeServiceTest
{
    [Fact]
    public void WHEN_asking_for_the_date_and_time_now_THEN_returns_the_utc_time()
    {
        var utcNow = DateTimeOffset.UtcNow;

        var sut = new DateTimeService();
        var result = sut.Now;

        result.Year.Should().Be(utcNow.Year);
        result.Month.Should().Be(utcNow.Month);
        result.Day.Should().Be(utcNow.Day);
        result.Hour.Should().Be(utcNow.Hour);
        result.Minute.Should().Be(utcNow.Minute);
        result.Second.Should().Be(utcNow.Second);
    }
}