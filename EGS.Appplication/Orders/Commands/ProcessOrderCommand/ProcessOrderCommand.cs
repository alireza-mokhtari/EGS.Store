using EGS.Application.Common.Interfaces;
using EGS.Application.Common.Models;
using EGS.Application.Dto;
using EGS.Application.Repositories;
using EGS.Domain.Entities;
using EGS.Domain.Enums;
using EGS.Domain.Events;
using MapsterMapper;

namespace EGS.Application.Orders.Commands.ProcessOrderCommand
{
    public class ProcessOrderCommand : IRequestWrapper<OrderDto>
    {
        //TODO: Consider later order manipulation by admin
        public long OrderId { get; set; }
        public bool IsVerified { get; set; }
    }

    public class ProcessOrderCommandHandler : IRequestHandlerWrapper<ProcessOrderCommand, OrderDto>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private readonly IDomainEventService _domainEventService;
        private readonly IInventoryRepository _inventoryRepository;

        public ProcessOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper,
            ICurrentUserService currentUserService, IDateTime dateTime,
            IDomainEventService domainEventService, IInventoryRepository inventoryRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _dateTime = dateTime;
            _domainEventService = domainEventService;
            _inventoryRepository = inventoryRepository;
        }

        public async Task<ServiceResult<OrderDto>> Handle(ProcessOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.FirstOrDefaultAsync(cancellationToken, o => o.Id == request.OrderId, enableTracking: false);
            order.OrderHistories.Add(BuildHistory(request));
            var res = _orderRepository.Update(order);
            var stocks = await _orderRepository.GetOrderStocks(request.OrderId);

            var inventoryTransactions = order.OrderItems.Select(o => new InventoryTransaction
            {
                BookId = o.BookId,
                Decremented = o.Quantity,
                ModifiedOn = _dateTime.Now,
                Stock = stocks.FirstOrDefault(s => s.BookId == o.BookId)?.Stock ?? 0,
                Reason = InventoryTransactionType.OrderCompletion,
                UserId = _currentUserService.UserId
            });

            _inventoryRepository.BulkInsert(inventoryTransactions);

            await _orderRepository.SaveChangesAsync(cancellationToken);
            //TODO: Move to Unit Of Work

            if (request.IsVerified)
                await _domainEventService.Publish(new OrderCompletedEvent(request.OrderId));

            return ServiceResult.Success(_mapper.Map<OrderDto>(res));
        }

        private OrderHistory BuildHistory(ProcessOrderCommand request)
        {
            var orderHistory = new OrderHistory();
            orderHistory.OrderId = request.OrderId;
            orderHistory.OrderStatus = request.IsVerified ? OrderStatus.Completed : OrderStatus.Cancelled;
            orderHistory.UserId = _currentUserService.UserId;
            orderHistory.OccuredAt = _dateTime.Now;
            return orderHistory;
        }
    }
}
