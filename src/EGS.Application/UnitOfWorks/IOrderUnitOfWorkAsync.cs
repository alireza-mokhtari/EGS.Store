using EGS.Application.Common.Interfaces;
using EGS.Application.Repositories;

namespace EGS.Application.UnitOfWorks
{
    public interface IOrderUnitOfWorkAsync : IUnitOfWorkAsync
    {
        IOrderRepository OrderRepository { get; }
        IInventoryRepository InventoryRepository { get; }
    }
}
