using EGS.Application.Common.Models;
using EGS.Domain.Common;
using Mapster;
using System.Linq.Expressions;

namespace EGS.Application.Common.Interfaces
{
    public interface ICrudRepositoryAsync<TEntity, TKey> : IReositoryAsync , IReadOnlyRepositoryAsync<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, new()        
    {

        TEntity Insert(TEntity entity);

        TEntity Update(TEntity entity);

        void Delete(TEntity entity);        
    }

    public interface IReositoryAsync
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }

    public interface IReadOnlyRepositoryAsync<TEntity, TKey> : IReositoryAsync
        where TEntity : class, IEntity<TKey>, new()
    {
        IQueryable<TEntity> AsQueryable();

        Task<TEntity> FirstOrDefaultAsync(CancellationToken cancellationToken,
            Expression<Func<TEntity, bool>> predicate = null,
            bool enableTracking = true,
            bool ignoreQueryFilters = false);

        Task<bool> AnyAsync(CancellationToken cancellationToken,
            Expression<Func<TEntity, bool>> predicate = null);

        public Task<PaginatedList<TOut>> GetPaginatedListAsync<TOut>(CancellationToken cancellationToken,
            IQueryable<TEntity> query,
            TypeAdapterConfig mapperConfig,
            int pageSize = 10,
            int pageNumber = 1) where TOut : class;
    }

    public interface IAppendOnlyRepositoryAsync<TEntity, TKey> : IReadOnlyRepositoryAsync<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, new()
    {
        TEntity Insert(TEntity entity);
    }
}
