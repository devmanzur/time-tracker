using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using TimeTracker.Core.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace TimeTracker.Core.Shared.Utils;

public static class EntityFrameworkExtensions
{
    public static IQueryable<TEntity> Read<TEntity>(
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
    
    public static Expression<Func<TEntity, bool>> BuildLambdaForFindByKey<TEntity>(int id) {
        var item = Expression.Parameter(typeof(TEntity), "entity");
        var prop = Expression.Property(item, typeof(TEntity).Name[..^1] + "Id");
        var value = Expression.Constant(id);
        var equal = Expression.Equal(prop, value);
        var lambda = Expression.Lambda<Func<TEntity, bool>>(equal, item);
        return lambda;
    }
}