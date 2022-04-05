using EGS.Application.Common.Interfaces;
using EGS.Application.Common.Models;
using EGS.Application.Dto;
using EGS.Application.Repositories;
using EGS.Domain.Entities;
using Mapster;
using MapsterMapper;
using System.Text.Json.Serialization;

namespace EGS.Application.ShoppingCart.Commands.AddToCartCommand
{
    public class AddToCartCommand : IRequestWrapper<ShoppingCartItemDto>, IRegister
    {        
        public string CustomerId { get; set; }
        public long BookId { get; set; }
        public int Quantity { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AddToCartCommand, ShoppingCartItem>();
        }
    }

    public class AddToCartCommandHandler : IRequestHandlerWrapper<AddToCartCommand, ShoppingCartItemDto>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;
        private readonly IDateTime _dateTime;

        public AddToCartCommandHandler(ICartRepository cartRepository, IMapper mapper, IDateTime dateTime, IBookRepository bookRepository)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _dateTime = dateTime;
            _bookRepository = bookRepository;
        }
        public async Task<ServiceResult<ShoppingCartItemDto>> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var cartItem = _mapper.Map<ShoppingCartItem>(request);
            var book = await _bookRepository.FirstOrDefaultAsync(cancellationToken, b => b.Id == request.BookId);

            cartItem.ExpiresAt = _dateTime.Now.AddDays(Constants.DAYS_TO_EXPIRE_CART_ITEM);
            cartItem.Price = book.Price;

            var res = _cartRepository.Insert(cartItem);
            await _cartRepository.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<ShoppingCartItemDto>(res));
        }
    }
}
