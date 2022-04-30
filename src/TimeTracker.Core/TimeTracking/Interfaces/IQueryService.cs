using TimeTracker.Core.Shared.Interfaces;
using TimeTracker.Core.Shared.Models;

namespace TimeTracker.Core.TimeTracking.Interfaces;

public interface IQueryService
{
    Task<PageResult<T>> GetPage<T, TE>(Segment segment)
        where T : BaseDto where TE : BaseEntity, ICrudEntity<T>;

    IQueryable<TE> Query<TE>() where TE : BaseEntity;
}