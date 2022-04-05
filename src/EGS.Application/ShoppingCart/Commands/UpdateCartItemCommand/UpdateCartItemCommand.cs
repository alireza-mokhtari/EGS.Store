using EGS.Application.Common.Interfaces;
using EGS.Application.Common.Models;
using EGS.Application.Dto;
using EGS.Application.Repositories;
using EGS.Domain.Entities;
using Mapster;
using MapsterMapper;

namespace EGS.Application.ShoppingCart.Commands.UpdateCartItemCommand
{
    public class UpdateCartItemCommand : IRequestWrapper<ShoppingCartItemDto>, IRegister
    {
        public long BookId { get; set; }
        public int Quantity { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UpdateCartItemCommand, ShoppingCartItem>();
        }
    }

    public class UpdateCartItemCommandHandler : IRequestHandlerWrapper<UpdateCartItemCommand, ShoppingCartItemDto>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        public UpdateCartItemCommandHandler(ICartRepository cartRepository, IMapper mapper, IDateTime dateTime, ICurrentUserService currentUserService)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<ServiceResult<ShoppingCartItemDto>> Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
        {
            var existingItem = await _cartRepository.FirstOrDefaultAsync(cancellationToken,
                c => c.BookId == request.BookId && c.CustomerId == _currentUserService.UserId, enableTracking: false);

            existingItem.Quantity = request.Quantity;
            var res = _cartRepository.Update(existingItem);
            await _cartRepository.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<ShoppingCartItemDto>(res));
        }
    }
}
