using System.Linq.Expressions;
using EGS.Application.Common.Interfaces;
using EGS.Application.Common.Models;
using EGS.Domain.Common;
using EGS.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EGS.Infrastructure.Persistence.Repositories
{
    public class BaseRepositoryAsync<TEntity, TKey> : IRepositoryAsync<TEntity, TKey> 
        where TEntity: class , IEntity<TKey> , new() 
        where TKey : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        #region Ctor
        public BaseRepositoryAsync(IApplicationDbContext context)
        {
            _context = context as DbContext;
            if (_context == null)
                throw new ArgumentNullException(nameof(context));

            _dbSet = _context.Set<TEntity>();
        } 
        #endregion

        #region Read

        protected virtual IQueryable<TEntity> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<TEntity> FirstOrDefaultAsync(CancellationToken cancellationToken,
           Expression<Func<TEntity, bool>> predicate = null,
           bool enableTracking = true,
           bool ignoreQueryFilters = false)
        {
            IQueryable<TEntity> query = _dbSet;

            if (!enableTracking) query = query.AsNoTracking();

            if (predicate != null) query = query.Where(predicate);

            if (ignoreQueryFilters) query = query.IgnoreQueryFilters();

            return await query.FirstOrDefaultAsync(cancellationToken);
        }


        public Task<PaginatedList<TEntity>> GetPaginatedListAsync(CancellationToken cancellationToken,
            Expression<Func<TEntity, bool>> predicate = null,
            int pageSize = 10,
            int pageNumber = 1,
            bool enableTracking = true)
        {
            IQueryable<TEntity> query = _dbSet;
            if (!enableTracking) query = query.AsNoTracking();

            if (predicate != null) query = query.Where(predicate);

            return query.PaginatedListAsync(pageNumber, pageSize, cancellationToken);
        }

        #endregion

        #region Write
        public TEntity Insert(TEntity entity)
        {
            var entry = _dbSet.Add(entity);

            return entry.Entity;
        }

        public TEntity Update(TEntity entity)
        {
            var entry = _dbSet.Update(entity);
            return entry.Entity;
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
        #endregion
    }
}
