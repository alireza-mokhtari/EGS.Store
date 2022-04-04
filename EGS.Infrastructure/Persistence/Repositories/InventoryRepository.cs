using EGS.Application.Common.Interfaces;
using EGS.Application.Repositories;
using EGS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EGS.Infrastructure.Persistence.Repositories
{
    public class InventoryRepository : AppendOnlyRepositoryAsync<InventoryTransaction, long>,
        IAppendOnlyRepositoryAsync<InventoryTransaction, long>,
        IInventoryRepository
    {
        public InventoryRepository(IApplicationDbContext context) : base(context)
        {
        }

        public async Task<int> GetStock(long bookId, CancellationToken cancellationToken)
        {
            var latestTransaction = await _dbSet.Where(b => b.Id == bookId)
                .OrderByDescending(b => b.Id)
                .FirstOrDefaultAsync();

            return latestTransaction != null ? latestTransaction.Stock : 0;
        }
    }
}
