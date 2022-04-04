using EGS.Application.Books.Helpers;
using EGS.Application.Common.Interfaces;
using EGS.Application.Common.Models;
using EGS.Application.Dto;
using EGS.Application.Repositories;
using EGS.Domain.Enums;
using MapsterMapper;

namespace EGS.Application.Books.Queries
{
    public class SearchBooksQuery : PageableQuery, IRequestWrapper<PaginatedList<BookDto>>
    {
        public string? Query { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public BookSortingFields Order { get; set; }
        public bool IsDescending { get; set; }
    }

    public class SearchBooksQueryHandler : IRequestHandlerWrapper<SearchBooksQuery, PaginatedList<BookDto>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public SearchBooksQueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<PaginatedList<BookDto>>> Handle(SearchBooksQuery request, CancellationToken cancellationToken)
        {
            var query = _bookRepository.AsQueryable();

            if (!string.IsNullOrEmpty(request.Query))
                query = query.Where(b => b.ISBN.Contains(request.Query)
                    || b.Author.Contains(request.Query)
                    || b.Title.Contains(request.Query));

            if (request.PriceFrom != null)
                query = query.Where(b => b.Price >= request.PriceFrom);

            if (request.PriceTo != null)
                query = query.Where(b => b.Price <= request.PriceTo);

            var order = OrderHelper.GetOrder(request.Order);
            query = (request.IsDescending) ? query.OrderByDescending(order) : query.OrderBy(order);

            var list = await _bookRepository.
                GetPaginatedListAsync<BookDto>(cancellationToken, query, _mapper.Config, request.PageSize, request.PageNumber);

            return list.Items.Any() ? ServiceResult.Success(list) : ServiceResult.Failed<PaginatedList<BookDto>>(ServiceError.NotFound);

        }
    }
}
