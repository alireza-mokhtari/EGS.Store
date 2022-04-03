using EGS.Application.Common.Interfaces;
using EGS.Application.Repositories;
using EGS.Domain.Entities;

namespace EGS.Infrastructure.Persistence.Repositories
{
    public class BookRepository : BaseRepositoryAsync<Book, long>, IBookRepository
    {
        public BookRepository(IApplicationDbContext context) : base(context)
        {
        }
    }
}
