namespace TimeTracker.Core.Shared.Models;

public class Segment
{
    private const int MinSegmentSize = 10;
    private const int MaxSegmentSize = 100;

    public Segment()
    {
        var size = Size switch
        {
            < MinSegmentSize => MinSegmentSize,
            > MaxSegmentSize => MaxSegmentSize,
            _ => Size
        };

        Size = size;

        Skip = Skip < 0 ? 0 : Skip;
    }

    public int Skip { get; set; }

    public int Size { get; set; }
}