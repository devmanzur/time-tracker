using System;
using System.Collections;
using System.Collections.Generic;
using AutoFixture;
using AutoFixture.Xunit2;
using Bogus;
using FluentAssertions;
using TimeTracker.Core.Shared.Exceptions;
using TimeTracker.Core.Shared.Interfaces;
using TimeTracker.Core.TimeTracking.Models.Entities;
using Xunit;

namespace Test.TimeTracker.Core.TimeTracking.Models;

public class ActivityTests
{
    private static readonly Faker Faker = new Faker();
    private static readonly Fixture Fixture = new Fixture();

    #region Test data generators

    private class ActivityTestInvalidData : IEnumerable<object?[]>
    {
        public IEnumerator<object?[]> GetEnumerator()
        {
            yield return new object?[] {null, null, null, null};
            yield return new object?[] {null, Fixture.Create<Category>(), CreateValidDuration(), CreateValidDate()};
            yield return new object?[]
                {CreateInValidEntity<Mandate>(), Fixture.Create<Category>(), CreateValidDuration(), CreateValidDate()};
            yield return new object?[] {Fixture.Create<Mandate>(), null, CreateValidDuration(), CreateValidDate()};
            yield return new object?[]
                {Fixture.Create<Mandate>(), CreateInValidEntity<Category>(), CreateValidDuration(), CreateValidDate()};
            yield return new object?[] {Fixture.Create<Mandate>(), Fixture.Create<Category>(), null, CreateValidDate()};
            yield return new object?[]
                {Fixture.Create<Mandate>(), Fixture.Create<Category>(), CreateInvalidDuration(), CreateValidDate()};
            yield return new object?[]
                {Fixture.Create<Mandate>(), Fixture.Create<Category>(), CreateInvalidDuration(), null};
            yield return new object?[]
                {Fixture.Create<Mandate>(), Fixture.Create<Category>(), CreateInvalidDuration(), CreateInvalidDate()};
            yield return new object?[]
                {Fixture.Create<Mandate>(), Fixture.Create<Category>(), CreateInvalidDuration(), default};
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    private static T CreateInValidEntity<T>() where T : BaseEntity, new()
    {
        return new T()
        {
            Id = Faker.Random.Number(1, 1000)
        };
    }

    private static Duration CreateValidDuration()
    {
        int hours = Faker.Random.Number(0, 100);
        int minutes = Faker.Random.Number(5, 60);
        int seconds = Faker.Random.Number(0, 60);
        return new Duration()
        {
            Hours = hours,
            Minutes = minutes,
            Seconds = seconds
        };
    }

    private static Duration CreateInvalidDuration()
    {
        int hours = 0;
        int minutes = Faker.Random.Number(0, 4);
        int seconds = Faker.Random.Number(0, 60);
        return new Duration()
        {
            Hours = hours,
            Minutes = minutes,
            Seconds = seconds
        };
    }

    private static DateTime CreateValidDate()
    {
        var divergence = Faker.Random.Number(1, 5);
        return DateTime.UtcNow.Add(TimeSpan.FromDays(divergence));
    }

    private static DateTime CreateInvalidDate()
    {
        var divergence = Faker.Random.Number(2, 5);
        return new DateTime(DateTime.UtcNow.Year - divergence, 1, 1);
    }

    #endregion

    [Theory]
    [ClassData(typeof(ActivityTestInvalidData))]
    public void ShouldFailToInitializeActivityWhenOneOrMorePropertiesAreInvalid(Mandate mandate, Category category,
        Duration duration, DateTime dateTime)
    {
        //when
        Action createActivity = () => new Activity(mandate, category, duration, dateTime);

        //then
        createActivity.Should().ThrowExactly<BusinessRuleViolationException>();
    }

    [Theory, AutoData]
    public void ShouldInitializeActivityWhenAllPropertiesAreValid(Mandate mandate, Category category,
        Duration duration)
    {
        //when
        Action createActivity = () => new Activity(mandate, category, duration, CreateValidDate());

        //then
        createActivity.Should().ThrowExactly<BusinessRuleViolationException>();
    }
}