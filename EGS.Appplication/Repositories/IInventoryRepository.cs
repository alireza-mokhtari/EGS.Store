using EGS.Application.Common.Interfaces;
using EGS.Domain.Entities;

namespace EGS.Application.Repositories
{
    public interface IInventoryRepository : IAppendOnlyRepositoryAsync<InventoryTransaction , long>
    {
        Task<int> GetStock(long bookId, CancellationToken cancellationToken);
    }
}
