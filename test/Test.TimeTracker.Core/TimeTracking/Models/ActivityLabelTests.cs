using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoFixture.Xunit2;
using Bogus;
using CSharpFunctionalExtensions;
using FluentAssertions;
using TimeTracker.Core.TimeTracking.Models.Dto;
using TimeTracker.Core.TimeTracking.Models.Entities;
using Xunit;

namespace Test.TimeTracker.Core.TimeTracking.Models;

public class ActivityLabelTests
{
    private static readonly Faker Faker = new Faker();
    
    
    #region Test data generators

    private class ActivityLabelTestInvalidData : IEnumerable<object?[]>
    {
        public IEnumerator<object?[]> GetEnumerator()
        {
            yield return new object?[] {null};
            yield return new object?[]
            {
                new ActivityLabelDto()
                {
                    Name = null,
                    ColorCode = null
                }
            };
            yield return new object?[]
            {
                new ActivityLabelDto()
                {
                    Name = Faker.Random.String(),
                    ColorCode = null
                }
            };
            yield return new object?[]
            {
                new ActivityLabelDto()
                {
                    Name = null,
                    ColorCode = ColorCode.Blue.ToString()
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    #endregion

    [Theory]
    [ClassData(typeof(ActivityLabelTestInvalidData))]
    public void ShouldFailToInitializeActivityLabelWhenOneOrMorePropertiesAreInvalid(ActivityLabelDto request)
    {
        //given
        var validator = new ActivityLabelDtoValidator();
        var actualValidationError = validator.Validate(request);

        //when
        var activityLabel = new ActivityLabel();
        var result = activityLabel.Initialize(request);

        //then
        result.Should().Be(Result.Failure(actualValidationError.Errors?.FirstOrDefault()?.ErrorMessage));
    }

    [Theory]
    [ClassData(typeof(ActivityLabelTestInvalidData))]
    public void ShouldFailToUpdateActivityLabelWhenOneOrMorePropertiesAreInvalid(ActivityLabelDto request)
    {
        //given
        var validator = new ActivityLabelDtoValidator();
        var actualValidationError = validator.Validate(request);

        //when
        var activityLabel = new ActivityLabel();
        var result = activityLabel.Update(request);

        //then
        result.Should().Be(Result.Failure(actualValidationError.Errors?.FirstOrDefault()?.ErrorMessage));
    }
    
    [Theory,AutoData]
    public void ShouldInitializeActivityLabelWhenPropertiesAreValid(ActivityLabel request)
    {
        //when
        var activityLabel = new ActivityLabel();
        var result = activityLabel.Initialize(new ActivityLabelDto()
        {
            Name = request.Name,
            ColorCode = request.ColorCode.ToString()
        });

        //then
        result.Should().Be(Result.Success());
    }
    
    [Theory,AutoData]
    public void ShouldUpdateActivityLabelWhenPropertiesAreValid(ActivityLabel request)
    {
        //when
        var activityLabel = new ActivityLabel();
        var result = activityLabel.Update(new ActivityLabelDto()
        {
            Name = request.Name,
            ColorCode = request.ColorCode.ToString()
        });

        //then
        result.Should().Be(Result.Success());
    }
}