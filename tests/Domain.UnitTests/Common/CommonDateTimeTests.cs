using CleanCompanyName.DDDMicroservice.Domain.Common;
using FluentAssertions;
using Xunit;

namespace CleanCompanyName.DDDMicroservice.Domain.UnitTests.Common;
public class CommonDateTimeTests
{
    [Fact]
    public void WHEN_asking_for_the_date_and_time_now_THEN_returns_the_utc_time()
    {
        var utcNow = DateTimeOffset.UtcNow;

        var sut = CommonDateTime.Now;

        sut.Year.Should().Be(utcNow.Year);
        sut.Month.Should().Be(utcNow.Month);
        sut.Day.Should().Be(utcNow.Day);
        sut.Hour.Should().Be(utcNow.Hour);
        sut.Minute.Should().Be(utcNow.Minute);
        sut.Second.Should().Be(utcNow.Second);
    }
}
