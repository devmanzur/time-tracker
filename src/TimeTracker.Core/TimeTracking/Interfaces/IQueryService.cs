using TimeTracker.Core.Shared.Interfaces;
using TimeTracker.Core.Shared.Models.Dto;

namespace TimeTracker.Core.TimeTracking.Interfaces;

public interface IQueryService
{
    Task<PageResult<T>> Paginate<T, TE>(Segment segment)
        where T : BaseDto where TE : BaseEntity, ICrudEntity<T>;

    IQueryable<TE> Query<TE>() where TE : BaseEntity;
}