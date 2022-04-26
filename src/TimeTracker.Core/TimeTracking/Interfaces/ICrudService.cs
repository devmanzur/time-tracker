﻿using CSharpFunctionalExtensions;
using TimeTracker.Core.Shared.Interfaces;
using TimeTracker.Core.Shared.Models;

namespace TimeTracker.Core.TimeTracking.Interfaces;

public interface ICrudService
{
    Task<Result<T>> CreateItem<T, TE>(T request) where T : BaseDto where TE : BaseEntity, ICrudEntity<T>, new();
    Task<Result<T>> UpdateItem<T, TE>(int id, T request) where T : BaseDto where TE : BaseEntity, ICrudEntity<T>;
    Task<Result<T>> FindById<T, TE>(int id) where T : BaseDto where TE : BaseEntity, ICrudEntity<T>;
    Task<Result> RemoveItem<T, TE>(int id) where T : BaseDto where TE : BaseEntity, ICrudEntity<T>;
}