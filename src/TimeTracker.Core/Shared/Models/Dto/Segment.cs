namespace TimeTracker.Core.Shared.Models.Dto;

public class Segment
{
    private int _skip;
    private int _size;
    public const int MinSegmentSize = 10;
    public const int MinSegmentSkip = 10;
    public const int MaxSegmentSize = 100;

    public Segment()
    {
        Size = GetValidOrDefaultSize(Size);

        Skip = GetValidOrDefaultSkip(Skip);
    }

    private int GetValidOrDefaultSkip(int skip)
    {
        return skip < MinSegmentSkip ? MinSegmentSkip : skip;
    }

    private int GetValidOrDefaultSize(int size)
    {
        return size switch
        {
            < MinSegmentSize => MinSegmentSize,
            > MaxSegmentSize => MaxSegmentSize,
            _ => size
        };
    }

    public int Skip
    {
        get => _skip;
        set => _skip = GetValidOrDefaultSkip(value);
    }

    public int Size
    {
        get => _size;
        set => _size = GetValidOrDefaultSize(value);
    }
}