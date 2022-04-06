using EGS.Application.Common.Interfaces;
using EGS.Application.Dto;
using EGS.Application.Repositories;
using EGS.Domain.Entities;
using EGS.Domain.Enums;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace EGS.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : CrudRepositoryAsync<Order, long>, IOrderRepository
    {
        private readonly IDateTime _dateTime;
        private readonly IMapper _mapper;
        public OrderRepository(IApplicationDbContext context, IDateTime dateTime, IMapper mapper) : base(context)
        {
            _dateTime = dateTime;
            _mapper = mapper;
        }

        public Task<List<OrderStockDto>> GetOrderStocks(long orderId)
        {
            return _context.Set<OrderItem>()
                .Where(o => o.OrderId == orderId)
                .Select(o => new
                {
                    LatestTransaction = o.Book.InventoryTransactions.OrderByDescending(t => t.Id).FirstOrDefault(),
                    o.BookId,
                    o.Quantity
                })
                .Select(o => new OrderStockDto
                {
                    OrderId = orderId,
                    BookId = o.BookId,
                    Stock = o.LatestTransaction != null ? o.LatestTransaction.Stock : 0,
                    OrderQuantity = o.Quantity
                }).ToListAsync();            
        }

        public Task<OrderStatus> GetOrderStatus(long orderId)
        {
            return _context.Set<OrderHistory>()
                .Where(o => o.OrderId == orderId)
                .OrderByDescending(o => o.Id)
                .Select(o => o.OrderStatus)
                .FirstOrDefaultAsync();
        }

        public Task<Order?> GetOrder(long orderId, CancellationToken cancellationToken)
        {
            return _dbSet.Include(o => o.OrderItems)
                .Include(o => o.OrderHistories)
                .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);
        }
    }
}
