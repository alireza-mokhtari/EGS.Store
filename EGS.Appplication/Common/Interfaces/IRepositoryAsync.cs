using EGS.Application.Common.Models;
using EGS.Domain.Common;
using System.Linq.Expressions;

namespace EGS.Application.Common.Interfaces
{
    public interface IRepositoryAsync<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, new()        
    {
        Task<TEntity> FirstOrDefaultAsync(CancellationToken cancellationToken,
            Expression<Func<TEntity, bool>> predicate = null,
            bool enableTracking = true,
            bool ignoreQueryFilters = false);

        Task<bool> AnyAsync(CancellationToken cancellationToken,
            Expression<Func<TEntity, bool>> predicate = null);

        public Task<PaginatedList<TEntity>> GetPaginatedListAsync(CancellationToken cancellationToken,
            Expression<Func<TEntity, bool>> predicate = null,
            int pageSize = 10,
            int pageNumber = 1,
            bool enableTracking = true);

        TEntity Insert(TEntity entity);

        TEntity Update(TEntity entity);

        void Delete(TEntity entity);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
