using System.Threading.Tasks;
using EGS.Domain.Common;

namespace EGS.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
