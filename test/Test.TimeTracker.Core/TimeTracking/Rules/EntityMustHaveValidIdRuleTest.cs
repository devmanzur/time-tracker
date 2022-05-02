using Bogus;
using FluentAssertions;
using TimeTracker.Core.TimeTracking.Models.Entities;
using TimeTracker.Core.TimeTracking.Rules;
using Xunit;

namespace Test.TimeTracker.Core.TimeTracking.Rules;

public class EntityMustHaveValidIdRuleTest
{
    private readonly Faker _faker;

    public EntityMustHaveValidIdRuleTest()
    {
        _faker = new Faker();
    }
    
    [Fact]
    public void ShouldBreakEntityMustHaveValidIdRuleWhenEntityHasNonPositiveId()
    {
        //given
        int invalidId = _faker.Random.Number(-100, 0);
        var entity = new Category()
        {
            Id = invalidId,
            Name = _faker.Name.JobTitle(),
            Priority = Priority.Must,
            ColorCode = ColorCode.Blue,
            IconUrl = _faker.Image.PicsumUrl()
        };
        
        //when
        var rule = new EntityMustHaveValidIdRule(entity,nameof(Category));
        
        //then
        rule.IsBroken().Should().BeTrue();
    }
    
    [Fact]
    public void ShouldPassEntityMustHaveValidIdRuleWhenEntityPositiveId()
    {
        //given
        int invalidId = _faker.Random.Number(1, 100);
        var entity = new Category()
        {
            Id = invalidId,
            Name = _faker.Name.JobTitle(),
            Priority = Priority.Must,
            ColorCode = ColorCode.Blue,
            IconUrl = _faker.Image.PicsumUrl()
        };
        
        //when
        var rule = new EntityMustHaveValidIdRule(entity,nameof(Category));
        
        //then
        rule.IsBroken().Should().BeFalse();
    }
}