namespace EGS.Application.Common.Interfaces
{
    public interface IUnitOfWorkAsync
    {
        Task<int> CommitAsync(CancellationToken cancellationToken);
    }
}
