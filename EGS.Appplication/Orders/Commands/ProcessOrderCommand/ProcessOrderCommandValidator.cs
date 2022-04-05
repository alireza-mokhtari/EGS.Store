using EGS.Application.Repositories;
using FluentValidation;

namespace EGS.Application.Orders.Commands.ProcessOrderCommand
{
    public class ProcessOrderCommandValidator : AbstractValidator<ProcessOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        public ProcessOrderCommandValidator(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;

            RuleFor(c => c.OrderId)
                .MustAsync(BeCheckedOut).WithMessage("The specified order is already processed")
                .MustAsync(HasEnoughStock).WithMessage("One or more books don't have enough stock");
        }

        private async Task<bool> HasEnoughStock(long orderId, CancellationToken cancellationToken)
        {
            var stocks = await _orderRepository.GetOrderStocks(orderId);
            return stocks.All(s => s.Stock >= s.OrderQuantity);
        }

        private async Task<bool> BeCheckedOut(long orderId, CancellationToken cancellationToken)
        {
            var status = await _orderRepository.GetOrderStatus(orderId);
            return status == Domain.Enums.OrderStatus.CheckedOut;
        }
    }

}
