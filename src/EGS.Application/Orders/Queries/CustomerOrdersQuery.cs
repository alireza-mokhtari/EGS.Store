using EGS.Application.Common.Interfaces;
using EGS.Application.Common.Models;
using EGS.Application.Dto;
using EGS.Application.Repositories;
using MapsterMapper;

namespace EGS.Application.Orders.Queries
{
    public class CustomerOrdersQuery : PageableQuery, IRequestWrapper<PaginatedList<OrderSummaryDto>>
    {
        public string CustomerId { get; set; }
    }

    public class CustomerOrdersQueryHandler : IRequestHandlerWrapper<CustomerOrdersQuery, PaginatedList<OrderSummaryDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public CustomerOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<PaginatedList<OrderSummaryDto>>> Handle(CustomerOrdersQuery request, CancellationToken cancellationToken)
        {
            var query = _orderRepository.AsQueryable();
            query = query.Where(o => o.CustomerId == request.CustomerId);

            var res = await _orderRepository.GetPaginatedListAsync<OrderSummaryDto>(cancellationToken, query, _mapper.Config, request.PageSize, request.PageNumber);

            return ServiceResult.Success(res);
        }
    }
}
