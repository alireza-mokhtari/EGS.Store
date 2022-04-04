using EGS.Application.Common.Interfaces;
using EGS.Application.Common.Models;
using EGS.Application.Dto;
using EGS.Application.Repositories;
using EGS.Domain.Entities;
using Mapster;
using MapsterMapper;

namespace EGS.Application.Books.Commands.DeleteBookCommand
{
    public class DeleteBookCommand : IRequestWrapper<BookDto>
    {
        public long Id { get; set; }
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
            var existingBook = await _bookRepository.FirstOrDefaultAsync(cancellationToken, b => b.Id == request.Id, enableTracking: false);

            _bookRepository.Delete(existingBook);
            await _bookRepository.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<BookDto>(existingBook));
        }
    }
}
