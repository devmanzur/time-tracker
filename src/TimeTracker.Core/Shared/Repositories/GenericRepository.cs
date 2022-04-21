using System.Linq.Expressions;
using TimeTracker.Core.Shared.Interfaces;
using TimeTracker.Core.Shared.Utils;
using Microsoft.EntityFrameworkCore;

namespace TimeTracker.Core.Shared.Repositories;

    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly IDbContext _context;

        private readonly DbSet<TEntity> _dbSet;

        protected GenericRepository(IDbContext context) {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
   
        public IQueryable<TEntity> All() {
            return _dbSet.AsTracking();
        }
        
        public IQueryable<TEntity> FindByInclude
        (Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties) {
            var query = GetAllIncluding(includeProperties);
            return query.Where(predicate);
        }

        public IQueryable<TEntity> GetAllIncluding
            (params Expression<Func<TEntity, object>>[] includeProperties) {
            var queryable = _dbSet.AsTracking();

            return includeProperties.Aggregate
                (queryable, (current, includeProperty) => current.Include(includeProperty));
        }
        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> results = _dbSet.AsTracking()
                .Where(predicate);
            return results;
        }

        public Task<TEntity?> FindByKey(int id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var aggregate = includeProperties.Aggregate
                (_dbSet.AsQueryable(), (current, includeProperty) => current.Include(includeProperty));
            
            var lambda = EntityFrameworkExtensions.BuildLambdaForFindByKey<TEntity>(id);
            return aggregate.SingleOrDefaultAsync(lambda);
        }

        public TEntity? Find(Expression<Func<TEntity?, bool>> predicate)
        {
            return _dbSet.AsNoTracking()
                .SingleOrDefault(predicate);
        }

        public Task<int> Insert(TEntity entity) {
            _dbSet.Add(entity);
            return Commit();
        }

        public Task<int> Update(TEntity entity) 
        {
            _dbSet.Attach(entity);
            _context.SetModified(entity);
            return Commit();
        }

        private async Task<int> Commit()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int id) 
        {
            var entity = await FindByKey(id);
            _dbSet.Remove(entity);
            return await Commit();
        }
    }
