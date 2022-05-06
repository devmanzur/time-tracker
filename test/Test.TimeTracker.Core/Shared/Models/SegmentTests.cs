using Bogus;
using FluentAssertions;
using TimeTracker.Core.Shared.Models.Dto;
using Xunit;

namespace Test.TimeTracker.Core.Shared.Models;

public class SegmentTests
{
    private static readonly Faker Faker = new Faker();


    [Fact]
    public void ShouldFallbackToDefaultValuesWhenInvalidParametersArePassed()
    {
        //given 
        var segment = new Segment()
        {
            Size = Faker.Random.Number(-50, Segment.MinSegmentSize),
            Skip = Faker.Random.Number(-50, -1)
        };

        segment.Size.Should().Be(Segment.MinSegmentSize);
        segment.Skip.Should().Be(Segment.MinSegmentSkip);
    }


    [Fact]
    public void ShouldRetainExactSegmentValuesWhenValidParametersArePassed()
    {
        //given 
        var segment = new Segment()
        {
            Size = Faker.Random.Number(Segment.MinSegmentSize, Segment.MaxSegmentSize),
            Skip = Faker.Random.Number(Segment.MinSegmentSkip, 10000)
        };

        segment.Size.Should().Be(segment.Size);
        segment.Skip.Should().Be(segment.Skip);
    }
}