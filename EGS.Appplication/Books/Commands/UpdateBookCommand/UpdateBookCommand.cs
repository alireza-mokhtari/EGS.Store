using EGS.Application.Common.Interfaces;
using EGS.Application.Common.Models;
using EGS.Application.Dto;
using EGS.Application.Repositories;
using EGS.Domain.Entities;
using Mapster;
using MapsterMapper;

namespace EGS.Application.Books.Commands.UpdateBookCommand
{
    public class UpdateBookCommand : IRequestWrapper<BookDto>, IRegister
    {
        public long? Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int? GenreId { get; set; }
        public decimal Price { get; set; }
        public DateTime PublishDate { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UpdateBookCommand, Book>();
        }
    }

    public class UpdateBookCommandHandler : IRequestHandlerWrapper<UpdateBookCommand, BookDto>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public UpdateBookCommandHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<BookDto>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = _mapper.Map<Book>(request);

            var existingBook = await _bookRepository.FirstOrDefaultAsync(cancellationToken, b => b.Id == request.Id, enableTracking: false);
            book.Creator = existingBook.Creator;
            book.CreateDate = existingBook.CreateDate;

            _bookRepository.Update(book);
            await _bookRepository.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<BookDto>(book));
        }
    }
}
