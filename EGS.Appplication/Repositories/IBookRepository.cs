using EGS.Application.Common.Interfaces;
using EGS.Application.Dto;
using EGS.Domain.Entities;

namespace EGS.Application.Repositories
{
    public interface IBookRepository : ICrudRepositoryAsync<Book , long>
    {
        Task<BookDto> GetByISBNAsync(string isbn, CancellationToken cancellationToken);
    }
}
