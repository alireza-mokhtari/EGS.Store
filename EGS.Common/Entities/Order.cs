using EGS.Domain.Common;
using EGS.Domain.Enums;

namespace EGS.Domain.Entities
{
    public class Order : IEntity<Guid>
    {        
        public Guid Id { get; set; }
        public string CustomerId { get; set; }
        public DateTime OrderedDateTime { get; set; }
        public OrderStatus OrderStatus { get; set; }

    }
}
