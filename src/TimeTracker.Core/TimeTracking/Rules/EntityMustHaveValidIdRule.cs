using TimeTracker.Core.Shared.Interfaces;
using TimeTracker.Core.Shared.Utils;

namespace TimeTracker.Core.TimeTracking.Rules;

public class EntityMustHaveValidIdRule  : IBusinessRule
{
    private readonly BaseEntity _entity;
    private readonly string _entityName;

    public EntityMustHaveValidIdRule(BaseEntity entity, string entityName)
    {
        _entity = entity;
        _entityName = entityName;
    }
    
    public bool IsBroken()
    {
        return _entity.HasNoValue() || _entity.Id < 1;
    }

    public string Message => $"Selected {_entityName} is not a valid entity";
}