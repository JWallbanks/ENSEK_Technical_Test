using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WebAPI.DAL
{
    public interface IRepositoryAsync<T> where T : class
    {

        #region FindAsync

        Task<T> FindAsync(int id);

        #endregion

        #region GetEntireListAsync
        // Assumed to be without tracking
        Task<IReadOnlyList<T>> GetEntireListAsync();
        #endregion

        #region Insert Functions

        Task<T> InsertAsync(T entity, CancellationToken cancellationToken = default);

        Task InsertAsync(params T[] entities);

        Task InsertAsync(IEnumerable<T> entities,
            CancellationToken cancellationToken = default);

        #endregion

        #region Update Functions
        Task Update(T entity);
        Task Update(params T[] entities);
        Task Update(IEnumerable<T> entities);
        #endregion

        #region Delete Functions
        Task Delete(T entity);

        Task Delete(params T[] entities);

        Task Delete(IEnumerable<T> entities);
        #endregion


    }
}