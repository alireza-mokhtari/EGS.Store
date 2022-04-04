    using EGS.Application.Books.Helpers;
using EGS.Application.Repositories;
using FluentValidation;

namespace EGS.Application.Books.Commands.CreateBookCommand
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        private readonly IBookRepository _bookRepository;
        public CreateBookCommandValidator(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;


            RuleFor(b => b.Title)
                .NotEmpty().WithMessage("Book Title is requried")
                .MaximumLength(200).WithMessage("Book Title's maximum length is 200 characters");

            RuleFor(b => b.ISBN)
                .NotEmpty().WithMessage("ISBN is required")
                .Must(isbn => ValidationHelpers.IsISBNValid(isbn)).WithMessage("ISBN is not valid")
                .MustAsync(BeUniqueISBN).WithMessage("ISBN is duplicate")
                ;

            RuleFor(b => b.GenreId)
                .NotNull().WithMessage("Genre must be specified");
        }

        private async Task<bool> BeUniqueISBN(string isbn, CancellationToken cancellationToken)
        {
            var isbnExists = await _bookRepository.AnyAsync(cancellationToken, book => book.ISBN == isbn);
            return !isbnExists;
        }
    }
}
