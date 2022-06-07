using System.Collections.Generic;
using FluentAssertions;
using FluentValidation.Results;
using TimeTracker.Core.Shared.Utils;
using Xunit;

namespace Test.TimeTracker.Core.Shared.Utils;

public class ValidationUtilsTests
{
    [Fact]
    public void ShouldReturnErrorsInSerializedFormatForListOfValidationFailures()
    {
        //given 
        var expectedSerializedErrorMessage = "Name:Name is invalid;Name:Name length is invalid;Age:Must be an adult";
        var errors = new List<ValidationFailure>()
        {
            new ValidationFailure("Name", "Name is invalid"),
            new ValidationFailure("Name", "Name length is invalid"),
            new ValidationFailure("Age", "Must be an adult"),
        };
        //when
        var resultingErrorMessage = errors.GetSerializedErrors();
        //then
        resultingErrorMessage.Should().BeEquivalentTo(expectedSerializedErrorMessage);
    }
    
    [Fact]
    public void ShouldReturnErrorsInProblemDetailFormatWhenValidSerializedMessageIsPassed()
    {
        //given 
        var expectedDictionary = new Dictionary<string, List<string>>()
        {
            {
                "Name", new List<string>()
                {
                    "Name is invalid",
                    "Name length is invalid"
                }
            },
            {
                "Age", new List<string>()
                {
                    "Must be an adult"
                }
            }
        };
        var errors = new List<ValidationFailure>()
        {
            new ValidationFailure("Name", "Name is invalid"),
            new ValidationFailure("Name", "Name length is invalid"),
            new ValidationFailure("Age", "Must be an adult"),
        };
        //when
        var serializedErrorMessage = errors.GetSerializedErrors();
        var resultingDictionary = ValidationUtils.GetProblemDetailFormattedErrors(serializedErrorMessage);
        //then
        resultingDictionary.Should().BeEquivalentTo(expectedDictionary);
    }
}