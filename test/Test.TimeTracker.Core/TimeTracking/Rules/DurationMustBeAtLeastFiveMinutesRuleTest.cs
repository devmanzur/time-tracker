using Bogus;
using FluentAssertions;
using TimeTracker.Core.TimeTracking.Models.Entities;
using TimeTracker.Core.TimeTracking.Rules;
using Xunit;

namespace Test.TimeTracker.Core.TimeTracking.Rules;

public class DurationMustBeAtLeastFiveMinutesRuleTest
{
    private readonly Faker _faker;

    public DurationMustBeAtLeastFiveMinutesRuleTest()
    {
        _faker = new Faker();
    }

    
    [Fact]
    public void ShouldBreakDurationMustBeAtLeastFiveMinutesRuleWhenDurationIsLessThanFiveMinutes()
    {
        //given
        int hours = 0;
        int minutes = _faker.Random.Number(0,4);
        int seconds = _faker.Random.Number(0,60);
        Duration actualDuration = new Duration()
        {
            Hours = hours,
            Minutes = minutes,
            Seconds = seconds
        };
        
        //when 
        var rule = new DurationMustBeAtLeastFiveMinutesRule(actualDuration);
        
        //then
        rule.IsBroken().Should().BeTrue();
    }

    
    [Fact]
    public void ShouldPassDurationMustBeAtLeastFiveMinutesRuleWhenDurationIsMoreThanFiveMinutes()
    {
        //given
        int hours = _faker.Random.Number(0,60);
        int minutes = _faker.Random.Number(5,60);
        int seconds = _faker.Random.Number(0,60);
        Duration actualDuration = new Duration()
        {
            Hours = hours,
            Minutes = minutes,
            Seconds = seconds
        };
        
        //when 
        var rule = new DurationMustBeAtLeastFiveMinutesRule(actualDuration);
        
        //then
        rule.IsBroken().Should().BeFalse();
    }
}