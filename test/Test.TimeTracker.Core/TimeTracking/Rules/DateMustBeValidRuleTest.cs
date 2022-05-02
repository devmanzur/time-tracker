using System;
using Bogus;
using FluentAssertions;
using TimeTracker.Core.TimeTracking.Rules;
using Xunit;

namespace Test.TimeTracker.Core.TimeTracking.Rules;

public class DateMustBeValidRuleTest
{
    private readonly Faker _faker;

    public DateMustBeValidRuleTest()
    {
        _faker = new Faker();
    }

    [Fact]
    public void ShouldBreakDateMustBeValidRuleWhenDateIsDefault()
    {
        //given
        var actualDate = new DateTime();

        //when
        var rule = new DateMustBeValidRule(actualDate);

        //then
        rule.IsBroken().Should().BeTrue();
    }

    [Fact]
    public void ShouldBreakDateMustBeValidRuleWhenDateIsMoreThanOneYearInPast()
    {
        //given
        var divergence = _faker.Random.Number(2, 5);
        var actualDate = new DateTime(DateTime.UtcNow.Year - divergence, 1, 1);

        //when
        var rule = new DateMustBeValidRule(actualDate);

        //then
        rule.IsBroken().Should().BeTrue();
    }

    [Fact]
    public void ShouldBreakDateMustBeValidRuleWhenDateIsInFuture()
    {
        //given
        var divergence = _faker.Random.Number(1, 5);
        var actualDate = DateTime.UtcNow.Add(TimeSpan.FromDays(divergence));

        //when
        var rule = new DateMustBeValidRule(actualDate);

        //then
        rule.IsBroken().Should().BeTrue();
    }

    [Fact]
    public void ShouldPassDateMustBeValidRuleWhenDateIsInValidRange()
    {
        //given
        var divergence = _faker.Random.Number(0, 100000);
        var actualDate = DateTime.UtcNow.Add(TimeSpan.FromSeconds(-divergence));

        //when
        var rule = new DateMustBeValidRule(actualDate);

        //then
        rule.IsBroken().Should().BeFalse();
    }
}