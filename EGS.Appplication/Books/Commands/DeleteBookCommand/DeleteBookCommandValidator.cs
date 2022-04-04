using EGS.Application.Repositories;
using FluentValidation;

namespace EGS.Application.Books.Commands.DeleteBookCommand
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        private readonly IBookRepository _bookRepository;
        public DeleteBookCommandValidator(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;

            RuleFor(b => b.Id)
                .NotNull().WithMessage("Book Id is not specified")
                .Must(NotBeInOrder);
        }

        private bool NotBeInOrder(long arg)
        {
            return true;
        }
        
    }
}
