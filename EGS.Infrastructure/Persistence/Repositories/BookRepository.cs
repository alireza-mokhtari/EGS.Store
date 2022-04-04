using EGS.Application.Common.Interfaces;
using EGS.Application.Repositories;
using EGS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EGS.Infrastructure.Persistence.Repositories
{
    public class BookRepository : BaseRepositoryAsync<Book, long>, IBookRepository
    {
        public BookRepository(IApplicationDbContext context) : base(context)
        {
        }

        public Task<Book> GetByISBNAsync(string isbn, CancellationToken cancellationToken)
        {
            return AsQueryable()
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(b => b.ISBN == isbn, cancellationToken);
        }
    }
}
