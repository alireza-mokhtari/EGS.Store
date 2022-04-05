using EGS.Application.Common.Interfaces;
using EGS.Application.Common.Models;
using EGS.Application.Dto;
using EGS.Application.Repositories;
using EGS.Domain.Entities;
using MapsterMapper;

namespace EGS.Application.Orders.Commands.CheckoutCartCommand
{
    public class CheckoutCartCommand : IRequestWrapper<OrderDto>
    {
        public string Comment { get; set; }
    }

    public class CheckoutCartCommandHandler : IRequestHandlerWrapper<CheckoutCartCommand, OrderDto>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        public CheckoutCartCommandHandler(ICartRepository cartRepository, IMapper mapper,
            IOrderRepository orderRepository, ICurrentUserService currentUserService, IDateTime dateTime)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _orderRepository = orderRepository;
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        public async Task<ServiceResult<OrderDto>> Handle(CheckoutCartCommand request, CancellationToken cancellationToken)
        {
            var cartItems = await _cartRepository.GetCartItems(_currentUserService.UserId, cancellationToken);
            var order = new Order();
            order.Comment = request.Comment;
            order.CustomerId = _currentUserService.UserId;
            order.OrderHistories.Add(new OrderHistory
            {
                UserId = _currentUserService.UserId,
                OccuredAt = _dateTime.Now,
                OrderStatus = Domain.Enums.OrderStatus.CheckedOut
            });
            order.OrderItems = _mapper.Map<List<OrderItem>>(cartItems);

            var res = _orderRepository.Insert(order);
            await _orderRepository.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<OrderDto>(res));
        }
    }
}
