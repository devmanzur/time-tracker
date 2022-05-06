using TimeTracker.Core.Shared.Interfaces;

namespace TimeTracker.Core.Shared.Models.Dto;

public class PageResult<T> where T : BaseDto
{
    public PageResult(List<T> items, Segment segment, int totalItems)
    {
        Items = items;
        TotalItems = totalItems;
        CurrentPage = (segment.Skip / segment.Size) + 1;
        PageSize = segment.Size;
    }

    public List<T> Items { get; private set; }
    public int CurrentPage { get; private set; }
    public int PageSize { get; private set; }
    public int TotalItems { get; private set; }
    public int TotalItemsViewed => CurrentPage * PageSize;
    public bool HasNextPage => TotalItems > TotalItemsViewed;
}