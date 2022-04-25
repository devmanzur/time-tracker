using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using TimeTracker.Core.Shared.Interfaces;
using TimeTracker.Core.TimeTracking.Persistence;

namespace TimeTracker.Core.TimeTracking.UseCases.CreateItem;

public class CreateItemCommand<T, TE> : IRequest<Result<T>> where TE : BaseEntity, ICrudEntity<T>, new() where T : BaseDto
{
    public T Data { get; }

    public CreateItemCommand(T data)
    {
        Data = data;
    }
}

public class CreateItemCommandHandler<T, TE> : IRequestHandler<CreateItemCommand<T, TE>, Result<T>>
    where TE : BaseEntity, ICrudEntity<T>, new() where T : BaseDto
{
    private readonly TimeTrackingContext _context;
    private readonly IMapper _mapper;

    public CreateItemCommandHandler(TimeTrackingContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<T>> Handle(CreateItemCommand<T, TE> request, CancellationToken cancellationToken)
    {
        var item = new TE();
        item.Initialize(request.Data);
        _context.Set<TE>().Add(item);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success(request.Data);
    }
}