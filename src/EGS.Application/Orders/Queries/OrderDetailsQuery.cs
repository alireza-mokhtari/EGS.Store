using EGS.Application.Common.Interfaces;
using EGS.Application.Common.Models;
using EGS.Application.Dto;
using EGS.Application.Repositories;
using EGS.Domain.Enums;
using MapsterMapper;

namespace EGS.Application.Books.Queries
{
    public class OrderDetailsQuery : PageableQuery, IRequestWrapper<OrderDto>
    {
        public long OrderId { get; set; }
    }

    public class OrderDetailsQueryandler : IRequestHandlerWrapper<OrderDetailsQuery, OrderDto>
    {
        private readonly IOrderRepository _ordersRepository;
        private readonly IMapper _mapper;
        public OrderDetailsQueryandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _ordersRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<OrderDto>> Handle(OrderDetailsQuery request, CancellationToken cancellationToken)
        {
            var order = await _ordersRepository.GetOrder(request.OrderId, cancellationToken);

            return order != null ? ServiceResult.Success(_mapper.Map<OrderDto>(order)) : ServiceResult.Failed<OrderDto>(ServiceError.NotFound);

        }
    }
}
