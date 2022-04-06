using EGS.Application.Common.Interfaces;
using EGS.Application.Common.Models;
using EGS.Application.Dto;
using EGS.Application.Repositories;
using EGS.Domain.Enums;
using MapsterMapper;

namespace EGS.Application.Books.Queries
{
    public class OrderHistoryQuery : IRequestWrapper<List<OrderHistoryItemDto>>
    {
        public long OrderId { get; set; }
    }

    public class OrderHistoryQueryHandler : IRequestHandlerWrapper<OrderHistoryQuery, List<OrderHistoryItemDto>>
    {
        private readonly IOrderRepository _ordersRepository;
        private readonly IMapper _mapper;
        public OrderHistoryQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _ordersRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<OrderHistoryItemDto>>> Handle(OrderHistoryQuery request, CancellationToken cancellationToken)
        {
            var items = await _ordersRepository.GetHistory(request.OrderId, cancellationToken);
            return items.Any() ? ServiceResult.Success(items) : ServiceResult.Failed<List<OrderHistoryItemDto>>(ServiceError.NotFound);

        }
    }
}
