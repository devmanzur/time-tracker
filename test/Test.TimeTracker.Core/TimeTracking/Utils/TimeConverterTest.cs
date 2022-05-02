using Bogus;
using FluentAssertions;
using TimeTracker.Core.TimeTracking.Models.Entities;
using TimeTracker.Core.TimeTracking.Utils;
using Xunit;

namespace Test.TimeTracker.Core.TimeTracking.Utils;

public class TimeConverterTest
{
    private readonly Faker _faker;

    public TimeConverterTest()
    {
        _faker = new Faker();
    }
        
    [Fact]
    public void ShouldReturnTheSameDurationWhenConvertedToSecondsAndBack()
    {
        //given
        int hours = _faker.Random.Number(0,60);
        int minutes = _faker.Random.Number(0,60);
        int seconds = _faker.Random.Number(0,60);
        Duration actualDuration = new Duration()
        {
            Hours = hours,
            Minutes = minutes,
            Seconds = seconds
        };

        //when
        var convertedValueToSeconds = TimeConverter.ToSeconds(actualDuration);
        var convertedValueToDuration = TimeConverter.ToDuration(convertedValueToSeconds);

        //then
        var isEqual = actualDuration == convertedValueToDuration;
        isEqual.Should().BeTrue();
    }
        
    [Fact]
    public void ShouldReturnTheSameSecondsWhenConvertedToDurationAndBack()
    {
        //given
        int actualSeconds = _faker.Random.Number(0,600);

        //when
        var convertedValueToDuration = TimeConverter.ToDuration(actualSeconds);
        var convertedValueToSeconds = TimeConverter.ToSeconds(convertedValueToDuration);

        //then
        var isEqual = actualSeconds == convertedValueToSeconds;
        isEqual.Should().BeTrue();
    }
}