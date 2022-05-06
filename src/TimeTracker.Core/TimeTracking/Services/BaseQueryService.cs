using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Core.Shared.Interfaces;
using TimeTracker.Core.Shared.Models;
using TimeTracker.Core.Shared.Utils;
using TimeTracker.Core.TimeTracking.Interfaces;

namespace TimeTracker.Core.TimeTracking.Services;

public class BaseQueryService<TDc> : IQueryService where TDc : DbContext
{
    private readonly TDc _context;
    private readonly IMapper _mapper;

    public BaseQueryService(TDc context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IQueryable<TE> Query<TE>() where TE : BaseEntity
    {
        return _context.Set<TE>().ReadOnly().AsQueryable();
    }

    public async Task<PageResult<T>> GetPage<T, TE>(Segment segment)
        where T : BaseDto where TE : BaseEntity, ICrudEntity<T>
    {
        var total = await _context.Set<TE>().CountAsync();

        var items = await _context.Set<TE>()
            .ReadOnly()
            .Segment(segment)
            .ToListAsync();

        return new PageResult<T>(_mapper.Map<List<TE>, List<T>>(items), segment, total);
    }
}