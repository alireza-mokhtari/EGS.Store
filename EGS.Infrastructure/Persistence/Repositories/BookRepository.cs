using EGS.Application.Common.Interfaces;
using EGS.Application.Dto;
using EGS.Application.Repositories;
using EGS.Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EGS.Infrastructure.Persistence.Repositories
{
    public class BookRepository : CrudRepositoryAsync<Book, long>, IBookRepository
    {
        public BookRepository(IApplicationDbContext context) : base(context)
        {
        }

        public async Task<BookDto> GetByISBNAsync(string isbn, CancellationToken cancellationToken)
        {
            var res = await AsQueryable()
                .Include(b => b.Genre)
                .Include(b => b.InventoryTransactions)
                .ProjectToType<BookDto>()                
                .FirstOrDefaultAsync(b => b.ISBN == isbn, cancellationToken);

            return res;
        }
    }
}
