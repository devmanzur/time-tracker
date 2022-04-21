namespace TimeTracker.Core.Shared.Interfaces;

public interface ISoftDeletable
{
    public bool IsSoftDeleted { get; set; }
}