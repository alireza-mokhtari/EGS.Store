using EGS.Application.Repositories;
using FluentValidation;

namespace EGS.Application.ShoppingCart.Commands.AddToCartCommand
{
    public class AddToCartCommandValidator : AbstractValidator<AddToCartCommand>
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly ICartRepository _cartRepository;
        public AddToCartCommandValidator(IInventoryRepository inventoryRepository, ICartRepository cartRepository)
        {
            _inventoryRepository = inventoryRepository;
            _cartRepository = cartRepository;

            CascadeMode = CascadeMode.Stop;

            // TODO: Add Limit Validation

            RuleFor(s => s.CustomerId)
                .NotEmpty().WithMessage("Customer is not specified");

            RuleFor(s => s.BookId)
                .MustAsync(BeNew).WithMessage("The book is already added");

            RuleFor(s => s.Quantity)
                .MustAsync(BeLessThanStock).WithMessage("Requested quantity is more than stock");                         
        }

        private async Task<bool> BeNew(AddToCartCommand command, long bookId, CancellationToken cancellationToken)
        {
            var exists = await _cartRepository.AnyAsync(cancellationToken, c => c.BookId == bookId && c.CustomerId == command.CustomerId);
            return !exists;
        }

        private async Task<bool> BeLessThanStock(AddToCartCommand command, int quantity, CancellationToken cancellationToken)
        {
            var stock = await _inventoryRepository.GetStock(command.BookId, cancellationToken);
            return stock >= quantity;
        }
    }
}
