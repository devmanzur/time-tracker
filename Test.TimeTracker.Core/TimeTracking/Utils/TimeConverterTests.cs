using Bogus;
using FluentAssertions;
using TimeTracker.Core.TimeTracking.Models.Entities;
using TimeTracker.Core.TimeTracking.Utils;
using Xunit;

namespace Test.TimeTracker.Core.TimeTracking.Utils;

public class TimeConverterTests
{
    private readonly Faker _faker;

    public TimeConverterTests()
    {
        _faker = new Faker();
    }
        
    [Fact]
    public void ConvertingDurationShouldNotChangeTheActualValue()
    {
        //given
        int hours = _faker.Random.Number(0,60);
        int minutes = _faker.Random.Number(0,60);
        int seconds = _faker.Random.Number(0,60);
        Duration duration = new Duration()
        {
            Hours = hours,
            Minutes = minutes,
            Seconds = seconds
        };

        //when
        var convertedValueToSeconds = TimeConverter.ToSeconds(duration);
        var convertedValueToDuration = TimeConverter.ToDuration(convertedValueToSeconds);

        //then
        var isEqual = duration == convertedValueToDuration;
        isEqual.Should().BeTrue();
    }
}