using EGS.Application.Common.Interfaces;

namespace EGS.Infrastructure.Persistence.UnitOfWorks
{
    public class UnitOfWorkAsync : IUnitOfWorkAsync
    {
        protected readonly IApplicationDbContext _context;
        public UnitOfWorkAsync(IApplicationDbContext context)
        {
            _context = context;
        }

        public Task<int> CommitAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
