using EGS.Application.Common.Interfaces;
using EGS.Application.Common.Models;
using EGS.Application.Dto;
using EGS.Application.Repositories;
using MapsterMapper;

namespace EGS.Application.Books.Queries
{
    public class GetPaginatedBooksQuery : PageableQuery, IRequestWrapper<PaginatedList<BookDto>>
    {
        public string? ISBN { get; set; }
        public string? Author { get; set; }
        public string? Title { get; set; }
        public int? GenreId { get; set; }
    }

    public class GetPaginatedBooksQueryHandler : IRequestHandlerWrapper<GetPaginatedBooksQuery, PaginatedList<BookDto>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public GetPaginatedBooksQueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<PaginatedList<BookDto>>> Handle(GetPaginatedBooksQuery request, CancellationToken cancellationToken)
        {
            var query = _bookRepository.AsQueryable();

            if (request.GenreId != null)
                query = query.Where(b => b.GenreId == request.GenreId);

            if (!string.IsNullOrEmpty(request.Title))
                query = query.Where(b => b.Title.Contains(request.Title));

            if (!string.IsNullOrEmpty(request.Author))
                query = query.Where(b => b.Author.Contains(request.Author));

            if (!string.IsNullOrEmpty(request.ISBN))
                query = query.Where(b => b.ISBN.Contains(request.ISBN));

            query = query.OrderBy(b => b.Title);

            var list = await _bookRepository.
                GetPaginatedListAsync<BookDto>(cancellationToken, query, _mapper.Config, request.PageSize, request.PageNumber);

            return list.Items.Any() ? ServiceResult.Success(list) : ServiceResult.Failed<PaginatedList<BookDto>>(ServiceError.NotFound);

        }
    }
}
