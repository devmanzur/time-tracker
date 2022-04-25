using CSharpFunctionalExtensions;
using TimeTracker.Core.Shared.Interfaces;

namespace TimeTracker.Core.TimeTracking.Interfaces;

public interface ICrudService
{
    Task<Result<T>> CreateItem<T, TE>(T request) where T : BaseDto where TE : BaseEntity, ICrudEntity<T>, new();
    Task<Result<T>> UpdateItem<T,TE>(int id,T request) where T : BaseDto where TE : BaseEntity, ICrudEntity<T>;
}