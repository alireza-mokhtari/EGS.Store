using EGS.Domain.Common;

namespace EGS.Domain.Events
{
    public class OrderCompletedEvent : DomainEvent
    {
        public long OrderId { get; set; }

        public OrderCompletedEvent(long orderId)
        {
            OrderId = orderId;
        }
    }
}
