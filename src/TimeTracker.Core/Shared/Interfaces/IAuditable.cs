namespace TimeTracker.Core.Shared.Interfaces;

public interface IAuditable
{
    public DateTime CreatedTime { get; set; }
    public string CreatedBy { get; set; }
    public DateTime LastModifiedTime { get; set; }
    public string LastModifiedBy { get; set; }
}