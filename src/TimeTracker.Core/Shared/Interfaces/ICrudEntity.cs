namespace TimeTracker.Core.Shared.Interfaces;

/**
 * CRUD entities must have public getters and setters
 */
public interface ICrudEntity<in T> where T: BaseDto
{
    public void Initialize(T dto);
    public void Update(T dto);
}