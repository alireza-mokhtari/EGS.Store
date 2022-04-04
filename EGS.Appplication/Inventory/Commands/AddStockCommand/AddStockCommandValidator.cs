using EGS.Application.Repositories;
using FluentValidation;

namespace EGS.Application.Inventory.Commands.AddStockCommand
{

    public class AddStockCommandValidator : AbstractValidator<AddStockCommand>
    {
        private readonly IBookRepository _bookRepository;
        public AddStockCommandValidator(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;

            RuleFor(c => c.BookId)
                .NotEmpty().WithMessage("Book id must be specified")
                .MustAsync(Exists).WithMessage("Specified book doesn't exist");

            RuleFor(c => c.Quantity)
                .GreaterThan(0).WithMessage("Receiving stock must be greater than zero");

        }

        private Task<bool> Exists(AddStockCommand request, long? bookId, CancellationToken cancellationToken)
        {
            return _bookRepository.AnyAsync(cancellationToken, b => b.Id == bookId);
        }
    }
}
