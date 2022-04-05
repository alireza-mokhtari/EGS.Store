using EGS.Application.Repositories;
using FluentValidation;

namespace EGS.Application.Inventory.Commands.ReduceStockCommand
{

    public class ReduceStockCommandValidator : AbstractValidator<ReduceStockCommand>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IInventoryRepository _inventoryRepository;
        public ReduceStockCommandValidator(IBookRepository bookRepository, IInventoryRepository inventoryRepository)
        {
            _bookRepository = bookRepository;
            _inventoryRepository = inventoryRepository;

            RuleFor(c => c.BookId)
                .NotEmpty().WithMessage("Book id must be specified")
                .MustAsync(Exists).WithMessage("Specified book doesn't exist");

            RuleFor(c => c.Quantity)
                .LessThan(0).WithMessage("Receiving stock must be less than zero")
                .MustAsync(LessOrEqualCurrentStock);
        }

        private async Task<bool> LessOrEqualCurrentStock(ReduceStockCommand request, int quantity, CancellationToken cancellationToken)
        {
            var currentStock = await _inventoryRepository.GetStock(request.BookId.Value, cancellationToken);
            return Math.Abs(currentStock) >= quantity;
        }

        private Task<bool> Exists(ReduceStockCommand request, long? bookId, CancellationToken cancellationToken)
        {
            return _bookRepository.AnyAsync(cancellationToken, b => b.Id == bookId);
        }
    }

}
