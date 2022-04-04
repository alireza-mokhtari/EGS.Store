using EGS.Application.Common.Interfaces;
using EGS.Application.Common.Models;
using EGS.Application.Dto;
using EGS.Application.Repositories;
using MapsterMapper;

namespace EGS.Application.Books.Commands.DeleteBookCommand
{
    public class DeleteBookCommand : IRequestWrapper<BookDto>
    {
        public string ISBN { get; set; }
    }

    public class DeleteBookCommandHandler : IRequestHandlerWrapper<DeleteBookCommand, BookDto>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public DeleteBookCommandHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<BookDto>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var existingBook = await _bookRepository.FirstOrDefaultAsync(cancellationToken, b => b.ISBN == request.ISBN, enableTracking: false);

            _bookRepository.Delete(existingBook);
            await _bookRepository.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<BookDto>(existingBook));
        }
    }
}
