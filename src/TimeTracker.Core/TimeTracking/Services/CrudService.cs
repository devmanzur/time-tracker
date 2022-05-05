using AutoMapper;
using CSharpFunctionalExtensions;
using TimeTracker.Core.Shared.Interfaces;
using TimeTracker.Core.Shared.Utils;
using TimeTracker.Core.TimeTracking.Interfaces;
using TimeTracker.Core.TimeTracking.Persistence;

namespace TimeTracker.Core.TimeTracking.Services;

public class CrudService : ICrudService
{
    private readonly TimeTrackingContext _context;
    private readonly IMapper _mapper;

    public CrudService(TimeTrackingContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<T>> CreateItem<T, TE>(T request)
        where T : BaseDto where TE : BaseEntity, ICrudEntity<T>, new()
    {
        var item = new TE();
        var initialize = item.Initialize(request);
        if (initialize.IsFailure)
        {
            return Result.Failure<T>(initialize.Error);
        }

        _context.Set<TE>().Add(item);
        await _context.SaveChangesAsync();
        request.Id = item.Id;
        return Result.Success(request);
    }

    public async Task<Result<T>> UpdateItem<T, TE>(int id, T request)
        where T : BaseDto where TE : BaseEntity, ICrudEntity<T>
    {
        Maybe<TE?> item = await _context.Set<TE>().FindById(id);
        if (item.HasNoValue)
        {
            return Result.Failure<T>("Item not found!");
        }

        var update = item.Value!.Update(request);
        if (update.IsFailure)
        {
            return Result.Failure<T>(update.Error);
        }

        await _context.SaveChangesAsync();
        return Result.Success(request);
    }

    public async Task<Result> RemoveItem<T, TE>(int id) where T : BaseDto where TE : BaseEntity, ICrudEntity<T>
    {
        Maybe<TE?> item = await _context.Set<TE>().FindById(id);
        if (item.HasNoValue)
        {
            return Result.Failure<T>("Item not found!");
        }

        _context.Remove(item.Value!);
        await _context.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result<T>> FindById<T, TE>(int id) where T : BaseDto where TE : BaseEntity, ICrudEntity<T>
    {
        Maybe<TE?> item = await _context.Set<TE>().ReadOnly().FindById(id);
        if (item.HasNoValue)
        {
            return Result.Failure<T>("Item not found!");
        }

        var data = _mapper.Map<TE, T>(item.Value!);
        return Result.Success(data);
    }
}