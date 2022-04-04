using EGS.Application.Books.Helpers;
using EGS.Application.Repositories;
using FluentValidation;

namespace EGS.Application.Books.Commands.UpdateBookCommand
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        private readonly IBookRepository _bookRepository;
        public UpdateBookCommandValidator(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;

            RuleFor(b => b.Id)
                .NotNull().WithMessage("Book Id is not specified");

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

        private async Task<bool> BeUniqueISBN(UpdateBookCommand book, string isbn, CancellationToken cancellationToken)
        {
            var isbnExists = await _bookRepository.AnyAsync(cancellationToken, b => b.ISBN == isbn && b.Id != book.Id);
            return !isbnExists;
        }
    }
}
