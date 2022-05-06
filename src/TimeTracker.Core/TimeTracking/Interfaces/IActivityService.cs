using CSharpFunctionalExtensions;
using TimeTracker.Core.Shared.Models.Dto;
using TimeTracker.Core.TimeTracking.Models.Dto;

namespace TimeTracker.Core.TimeTracking.Interfaces;

public interface IActivityService
{
    Task<Result<ActivityDetailsDto>> Create(ActivityDto request);
    Task<Result<ActivityDetailsDto>> Update(int id, ActivityDto request);
    Task<Result<ActivityDetailsDto?>> Get(int id);
    Task<Result<ActivityDetailsDto>> Remove(int id);
    Task<PageResult<ActivityDetailsDto>> GetPage(Segment segment);
}