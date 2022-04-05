using EGS.Application.Common.Interfaces;
using EGS.Application.Common.Models;
using EGS.Application.Dto;
using EGS.Application.Repositories;
using MapsterMapper;

namespace EGS.Application.ShoppingCart.Commands.DeleteCartItemCommand
{
    public class DeleteCartItemCommand : IRequestWrapper<ShoppingCartItemDto>
    {
        public long BookId { get; set; }
    }

    public class DeleteCartItemCommandHandler : IRequestHandlerWrapper<DeleteCartItemCommand, ShoppingCartItemDto>
    {
        private readonly IMapper _mapper;
        private readonly ICartRepository _cartRepository;
        public DeleteCartItemCommandHandler(IMapper mapper, ICartRepository cartRepository)
        {
            _mapper = mapper;
            _cartRepository = cartRepository;
        }

        public async Task<ServiceResult<ShoppingCartItemDto>> Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _cartRepository.FirstOrDefaultAsync(cancellationToken, c => c.BookId == request.BookId);
            _cartRepository.Delete(item);
            await _cartRepository.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<ShoppingCartItemDto>(item));
        }
    }
}
