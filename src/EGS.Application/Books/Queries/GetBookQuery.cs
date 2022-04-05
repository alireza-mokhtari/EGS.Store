using EGS.Application.Common.Interfaces;
using EGS.Application.Common.Models;
using EGS.Application.Dto;
using EGS.Application.Repositories;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGS.Application.Books.Queries
{
    public class GetBookQuery : IRequestWrapper<BookDto>
    {
        public string ISBN { get; set; }
    }

    public class GetBookQueryHandler : IRequestHandlerWrapper<GetBookQuery, BookDto>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public GetBookQueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<BookDto>> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByISBNAsync(request.ISBN, cancellationToken);
            
            var dto = _mapper.Map<BookDto>(book);            

            return ServiceResult.Success(dto);
        }
    }
}
