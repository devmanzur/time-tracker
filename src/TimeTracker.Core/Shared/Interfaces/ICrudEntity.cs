using CSharpFunctionalExtensions;

namespace TimeTracker.Core.Shared.Interfaces;

/**
 * CRUD entities must have public getters and setters
 */
public interface ICrudEntity<in T> where T: BaseDto
{
    public Result Initialize(T dto);
    public Result Update(T dto);
}