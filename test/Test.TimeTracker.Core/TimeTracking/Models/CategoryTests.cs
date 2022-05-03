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

public class CategoryTests
{
    private static readonly Faker Faker = new Faker();
    
    
    #region Test data generators

    private class CategoryTestInvalidData : IEnumerable<object?[]>
    {
        public IEnumerator<object?[]> GetEnumerator()
        {
            yield return new object?[] {null};
            yield return new object?[]
            {
                new CategoryDto()
                {
                    Name = null,
                    ColorCode = null,
                    Priority = null,
                    IconUrl = null
                }
            };
            yield return new object?[]
            {
                new CategoryDto()
                {
                    Name = null,
                    ColorCode = ColorCode.Blue.ToString(),
                    Priority = Priority.Must.ToString(),
                    IconUrl = Faker.Random.String()
                }
            };
            yield return new object?[]
            {
                new CategoryDto()
                {
                    Name = Faker.Random.String(),
                    ColorCode = null,
                    Priority = Priority.Must.ToString(),
                    IconUrl = Faker.Random.String()
                }
            };
            yield return new object?[]
            {
                new CategoryDto()
                {
                    Name = Faker.Random.String(),
                    ColorCode = ColorCode.Blue.ToString(),
                    Priority = null,
                    IconUrl = Faker.Random.String()
                }
            };
            yield return new object?[]
            {
                new CategoryDto()
                {
                    Name = Faker.Random.String(),
                    ColorCode = ColorCode.Blue.ToString(),
                    Priority = Priority.Must.ToString(),
                    IconUrl = null
                }
            };
            yield return new object?[]
            {
                new CategoryDto()
                {
                    Name = Faker.Random.String(),
                    ColorCode = ColorCode.Blue.ToString(),
                    Priority = ColorCode.Green.ToString(),
                    IconUrl = Faker.Random.String()
                }
            };
            yield return new object?[]
            {
                new CategoryDto()
                {
                    Name = Faker.Random.String(),
                    ColorCode = Priority.Want.ToString(),
                    Priority = Priority.Must.ToString(),
                    IconUrl = Faker.Random.String()
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    #endregion

    [Theory]
    [ClassData(typeof(CategoryTestInvalidData))]
    public void ShouldFailToInitializeCategoryWhenOneOrMorePropertiesAreInvalid(CategoryDto request)
    {
        //given
        var validator = new CategoryDtoValidator();
        var actualValidationError = validator.Validate(request);

        //when
        var activityLabel = new Category();
        var result = activityLabel.Initialize(request);

        //then
        result.Should().Be(Result.Failure(actualValidationError.Errors?.FirstOrDefault()?.ErrorMessage));
    }

    [Theory]
    [ClassData(typeof(CategoryTestInvalidData))]
    public void ShouldFailToUpdateCategoryWhenOneOrMorePropertiesAreInvalid(CategoryDto request)
    {
        //given
        var validator = new CategoryDtoValidator();
        var actualValidationError = validator.Validate(request);

        //when
        var activityLabel = new Category();
        var result = activityLabel.Update(request);

        //then
        result.Should().Be(Result.Failure(actualValidationError.Errors?.FirstOrDefault()?.ErrorMessage));
    }
    
    [Theory,AutoData]
    public void ShouldInitializeCategoryWhenPropertiesAreValid(Category request)
    {
        //when
        var activityLabel = new Category();
        var result = activityLabel.Initialize(new CategoryDto()
        {
            Name = request.Name,
            ColorCode = request.ColorCode.ToString(),
            Priority = request.Priority.ToString(),
            IconUrl = request.IconUrl
        });

        //then
        result.Should().Be(Result.Success());
    }
    
    [Theory,AutoData]
    public void ShouldUpdateCategoryWhenPropertiesAreValid(Category request)
    {
        //when
        var activityLabel = new Category();
        var result = activityLabel.Update(new CategoryDto()
        {
            Name = request.Name,
            ColorCode = request.ColorCode.ToString(),
            Priority = request.Priority.ToString(),
            IconUrl = request.IconUrl
        });

        //then
        result.Should().Be(Result.Success());
    }
}