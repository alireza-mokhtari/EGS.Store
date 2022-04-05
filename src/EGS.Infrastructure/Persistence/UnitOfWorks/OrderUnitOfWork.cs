using EGS.Application.Common.Interfaces;
using EGS.Application.Repositories;
using EGS.Application.UnitOfWorks;

namespace EGS.Infrastructure.Persistence.UnitOfWorks
{

    public class OrderUnitOfWork : UnitOfWorkAsync, IOrderUnitOfWorkAsync
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IInventoryRepository _inventoryRepository;
        public OrderUnitOfWork(IApplicationDbContext context,
            IOrderRepository orderRepository, IInventoryRepository inventoryRepository) : base(context)
        {
            _orderRepository = orderRepository;
            _inventoryRepository = inventoryRepository;
        }

        public IOrderRepository OrderRepository => _orderRepository;

        public IInventoryRepository InventoryRepository => _inventoryRepository;
    }
}
