using EGS.Domain.Common;
using EGS.Domain.Enums;

namespace EGS.Domain.Entities
{
    public class ShoppingCartItem : IEntity<long>
    {
        public long Id { get; set; }
        public string CustomerId { get; set; }
        public long BookId { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime ExpiresAt { get; set; }
    }

    public class ShoppingCartHistory : IEntity<long>
    {
        public long Id { get; set; }
        public long BookId { get; set; }
        public ShoppingCartHistoryAction Action { get; set; }
        public string CustomerId { get; set; }        
        public DateTime OccuredAt { get; set; }        
    }
}
