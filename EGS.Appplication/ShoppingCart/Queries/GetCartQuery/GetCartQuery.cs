using EGS.Application.Common.Interfaces;
using EGS.Application.Common.Models;
using EGS.Application.Dto;
using EGS.Application.Repositories;
using MapsterMapper;

namespace EGS.Application.ShoppingCart.Queries.GetCartQuery
{
    public class GetCartQuery :IRequestWrapper<PaginatedList<ShoppingCartItemDto>>
    {
        public string CustomerId { get; set; }
    }

    public class GetCartItemQueryHandler : IRequestHandlerWrapper<GetCartQuery, PaginatedList<ShoppingCartItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICartRepository _cartRepository;
        private readonly IDateTime _dateTime;

        public GetCartItemQueryHandler(IMapper mapper, ICartRepository cartRepository, IDateTime dateTime)
        {
            _mapper = mapper;
            _cartRepository = cartRepository;
            _dateTime = dateTime;
        }

        public async Task<ServiceResult<PaginatedList<ShoppingCartItemDto>>> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var query = _cartRepository.AsQueryable();

            query = query.Where(c => c.CustomerId == request.CustomerId && c.ExpiresAt >= _dateTime.Now);

            var list = await _cartRepository.GetPaginatedListAsync<ShoppingCartItemDto>(cancellationToken,
                query,
                _mapper.Config,
                Constants.CART_LIMIT,
                1);

            return list.Items.Any() ? ServiceResult.Success(list) : ServiceResult.Failed<PaginatedList<ShoppingCartItemDto>>(ServiceError.NotFound);

        }
    }
}
