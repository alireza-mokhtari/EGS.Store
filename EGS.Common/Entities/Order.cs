using EGS.Domain.Common;
using EGS.Domain.Enums;

namespace EGS.Domain.Entities
{
    public class Order : IEntity<long>
    {
        public Order()
        {
            OrderItems = new List<OrderItem>();
            OrderHistories = new List<OrderHistory>();
        }

        public long Id { get; set; }
        public string CustomerId { get; set; }        
        public string Comment { get; set; }
        public IList<OrderItem> OrderItems { get; set; }
        public IList<OrderHistory> OrderHistories { get; set; }
    }

    public class OrderItem : IEntity<long>
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public Order Order { get; set; }
        public long BookId { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class OrderHistory : IEntity<long>
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public Order Order { get; set; }
        public string UserId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime OccuredAt { get; set; }
    }
}
