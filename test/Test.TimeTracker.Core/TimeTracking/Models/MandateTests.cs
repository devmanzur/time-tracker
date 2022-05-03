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

public class MandateTests
{
    private static readonly Faker Faker = new Faker();
    
    
    #region Test data generators

    private class MandateTestInvalidData : IEnumerable<object?[]>
    {
        public IEnumerator<object?[]> GetEnumerator()
        {
            yield return new object?[] {null};
            yield return new object?[]
            {
                new MandateDto()
                {
                    Name = null,
                    ColorCode = null
                }
            };
            yield return new object?[]
            {
                new MandateDto()
                {
                    Name = Faker.Random.String(),
                    ColorCode = null
                }
            };
            yield return new object?[]
            {
                new MandateDto()
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
    [ClassData(typeof(MandateTestInvalidData))]
    public void ShouldFailToInitializeMandateWhenOneOrMorePropertiesAreInvalid(MandateDto request)
    {
        //given
        var validator = new MandateDtoValidator();
        var actualValidationError = validator.Validate(request);

        //when
        var activityLabel = new Mandate();
        var result = activityLabel.Initialize(request);

        //then
        result.Should().Be(Result.Failure(actualValidationError.Errors?.FirstOrDefault()?.ErrorMessage));
    }

    [Theory]
    [ClassData(typeof(MandateTestInvalidData))]
    public void ShouldFailToUpdateMandateWhenOneOrMorePropertiesAreInvalid(MandateDto request)
    {
        //given
        var validator = new MandateDtoValidator();
        var actualValidationError = validator.Validate(request);

        //when
        var activityLabel = new Mandate();
        var result = activityLabel.Update(request);

        //then
        result.Should().Be(Result.Failure(actualValidationError.Errors?.FirstOrDefault()?.ErrorMessage));
    }
    
    [Theory,AutoData]
    public void ShouldInitializeMandateWhenPropertiesAreValid(Mandate request)
    {
        //when
        var activityLabel = new Mandate();
        var result = activityLabel.Initialize(new MandateDto()
        {
            Name = request.Name,
            ColorCode = request.ColorCode.ToString()
        });

        //then
        result.Should().Be(Result.Success());
    }
    
    [Theory,AutoData]
    public void ShouldUpdateMandateWhenPropertiesAreValid(Mandate request)
    {
        //when
        var activityLabel = new Mandate();
        var result = activityLabel.Update(new MandateDto()
        {
            Name = request.Name,
            ColorCode = request.ColorCode.ToString()
        });

        //then
        result.Should().Be(Result.Success());
    }
}