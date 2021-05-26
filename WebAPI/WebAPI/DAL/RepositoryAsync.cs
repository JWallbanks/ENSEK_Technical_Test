using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.DAL
{
    public class RepositoryAsync<T> : IRepositoryAsync<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        public RepositoryAsync(EnsekContext dbContext)
        {
            _dbSet = dbContext.Set<T>();
        }

        #region FindAsync

        public virtual async Task<T> FindAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        #endregion

        #region GetEntireListAsync

        public virtual async Task<IReadOnlyList<T>> GetEntireListAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }
        #endregion

        #region Insert Functions

        public virtual async Task<T> InsertAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            return entity;
        }


        public virtual async Task InsertAsync(params T[] entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }


        public virtual async Task InsertAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
        }

        #endregion

        #region Update Functions

        public virtual Task Update(T entity)
        {
            _dbSet.Update(entity);
            return Task.FromResult(true);
        }

        public virtual Task Update(params T[] entities)
        {
            _dbSet.UpdateRange(entities);
            return Task.FromResult(true);
        }

        public virtual Task Update(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
            return Task.FromResult(true);
        }

        #endregion

        #region Delete Functions
        public virtual Task Delete(T entity)
        {
            _dbSet.Remove(entity);
            return Task.FromResult(true);

        }

        public virtual Task Delete(params T[] entities)
        {
            _dbSet.RemoveRange(entities);
            return Task.FromResult(true);

        }

        public virtual Task Delete(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            return Task.FromResult(true);

        }
        #endregion
    }
}