using EGS.Application.Common.Interfaces;
using EGS.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EGS.Infrastructure.Persistence.Repositories
{
    public class CrudRepositoryAsync<TEntity, TKey> : AppendOnlyRepositoryAsync<TEntity, TKey>, ICrudRepositoryAsync<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, new()
    {
        #region Ctor
        public CrudRepositoryAsync(IApplicationDbContext context) : base(context)
        {
        }
        #endregion        

        #region Write

        public virtual TEntity Update(TEntity entity)
        {
            var entry = _dbSet.Update(entity);
            return entry.Entity;
        }

        public virtual void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
        #endregion
    }
}
