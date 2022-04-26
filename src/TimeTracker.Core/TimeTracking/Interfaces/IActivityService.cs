using CSharpFunctionalExtensions;
using TimeTracker.Core.TimeTracking.Models.Dto;

namespace TimeTracker.Core.TimeTracking.Interfaces;

public interface IActivityService
{
    Task<Result<ActivityDto>> Create(ActivityDto request);
    Task<Result<ActivityDto>> Update(int id, ActivityDto request);
    Task<Result<ActivityDto>> FindById(int id);
    Task<Result> Remove(int id);
}