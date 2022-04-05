using EGS.Application.Common.Interfaces;
using EGS.Application.Common.Models;
using EGS.Application.Dto;
using EGS.Application.Repositories;
using EGS.Domain.Entities;
using Mapster;
using MapsterMapper;

namespace EGS.Application.Books.Commands.CreateBookCommand
{
    public class CreateBookCommand : IRequestWrapper<BookDto> , IRegister
    {        
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int? GenreId { get; set; }                
        public decimal Price { get; set; }
        public DateTime PublishDate { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateBookCommand, Book>();                
        }
    }

    public class CreateBookCommandHandler : IRequestHandlerWrapper<CreateBookCommand, BookDto>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public CreateBookCommandHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<BookDto>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = _mapper.Map<Book>(request);

            _bookRepository.Insert(book);
            await _bookRepository.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<BookDto>(book));
        }
    }
}
