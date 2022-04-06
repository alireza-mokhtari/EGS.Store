using EGS.Application.Common.Interfaces;
using EGS.Application.Common.Models;
using EGS.Application.Dto;
using EGS.Application.Repositories;
using EGS.Domain.Enums;
using MapsterMapper;

namespace EGS.Application.Books.Queries
{
    public class GetPaginatedOrdersQuery : PageableQuery, IRequestWrapper<PaginatedList<OrderSummaryDto>>
    {
        public string? CustomerId { get; set; }
        public DateTime? OrderedFrom { get; set; }
        public DateTime? OrderedTo { get; set; }
    }

    public class GetPaginatedOrdersQueryHandler : IRequestHandlerWrapper<GetPaginatedOrdersQuery, PaginatedList<OrderSummaryDto>>
    {
        private readonly IOrderRepository _ordersRepository;
        private readonly IMapper _mapper;
        public GetPaginatedOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _ordersRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<PaginatedList<OrderSummaryDto>>> Handle(GetPaginatedOrdersQuery request, CancellationToken cancellationToken)
        {
            var query = _ordersRepository.AsQueryable();

            if (request.CustomerId != null)
                query = query.Where(o => o.CustomerId == request.CustomerId);

            if (request.OrderedFrom != null)
                query = query.Where(o => o.OrderHistories.FirstOrDefault(o => o.OrderStatus == OrderStatus.CheckedOut).OccuredAt >= request.OrderedFrom);

            if (request.OrderedTo != null)
                query = query.Where(o => o.OrderHistories.FirstOrDefault(o => o.OrderStatus == OrderStatus.CheckedOut).OccuredAt <= request.OrderedTo);


            query = query.OrderByDescending(o => o.Id);

            var list = await _ordersRepository.
                GetPaginatedListAsync<OrderSummaryDto>(cancellationToken, query, _mapper.Config, request.PageSize, request.PageNumber);

            return list.Items.Any() ? ServiceResult.Success(list) : ServiceResult.Failed<PaginatedList<OrderSummaryDto>>(ServiceError.NotFound);

        }
    }
}
