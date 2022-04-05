using EGS.Application.Repositories;
using EGS.Domain.Entities;
using FluentValidation;

namespace EGS.Application.ShoppingCart.Commands.UpdateCartItemCommand
{
    public class UpdateCartItemCommandValidator : AbstractValidator<UpdateCartItemCommand>
    {
        private readonly IInventoryRepository _inventoryRepository;
        public UpdateCartItemCommandValidator(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;

            RuleFor(s => s.Quantity)
                .MustAsync(BeLessThanStock).WithMessage("Requested quantity is more than stock");
        }

        private async Task<bool> BeLessThanStock(UpdateCartItemCommand command, int quantity, CancellationToken cancellationToken)
        {
            var stock = await _inventoryRepository.GetStock(command.BookId, cancellationToken);
            return stock >= quantity;
        }
    }
}
