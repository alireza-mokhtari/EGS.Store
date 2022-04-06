using EGS.Application.Common.Interfaces;
using EGS.Application.Dto;
using EGS.Domain.Entities;
using EGS.Domain.Enums;
using Mapster;

namespace EGS.Application.Repositories
{
    public interface IOrderRepository : ICrudRepositoryAsync<Order, long>
    {
        Task<OrderStatus> GetOrderStatus(long orderId);
        Task<List<OrderStockDto>> GetOrderStocks(long orderId);
        Task<Order?> GetOrder(long orderId, CancellationToken cancellationToken);
        Task<List<OrderHistoryItemDto>> GetHistory(long orderId , CancellationToken cancellationToken);
    }
}
