using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using TimeTracker.Core.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TimeTracker.Core.Shared.Interfaces;

namespace TimeTracker.Core.Shared.Utils;

public static class EntityFrameworkExtensions
{
    public static void AddIgnoreSoftDeletedItemQueryFilter(
        this IMutableEntityType entityData)
    {
        var methodToCall = typeof(EntityFrameworkExtensions)
            .GetMethod(nameof(GetSoftDeleteFilter),
                BindingFlags.NonPublic | BindingFlags.Static)
            ?.MakeGenericMethod(entityData.ClrType);
        var filter = methodToCall?.Invoke(null, Array.Empty<object>());
        entityData.SetQueryFilter((LambdaExpression)filter!);
        entityData.AddIndex(entityData.FindProperty(nameof(ISoftDeletable.IsSoftDeleted))!);
    }

    private static LambdaExpression GetSoftDeleteFilter<TEntity>()
        where TEntity : class, ISoftDeletable
    {
        Expression<Func<TEntity, bool>> filter = x => !x.IsSoftDeleted;
        return filter;
    }

    public static IQueryable<TEntity> ReadOnly<TEntity>(
        [NotNull] this IQueryable<TEntity> source)
        where TEntity : class
    {
        return source.AsNoTracking();
    }

    public static IQueryable<TEntity> Segment<TEntity>(
        [NotNull] this IQueryable<TEntity> source, Segment segment)
        where TEntity : class
    {
        return source.Skip(segment.Skip).Take(segment.Size);
    }

    public static Expression<Func<TEntity, bool>> BuildLambdaForFindByKey<TEntity>(int id)
    {
        var item = Expression.Parameter(typeof(TEntity), "entity");
        var prop = Expression.Property(item, typeof(TEntity).Name[..^1] + "Id");
        var value = Expression.Constant(id);
        var equal = Expression.Equal(prop, value);
        var lambda = Expression.Lambda<Func<TEntity, bool>>(equal, item);
        return lambda;
    }
}