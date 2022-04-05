using EGS.Domain.Common;
using EGS.Domain.Enums;

namespace EGS.Domain.Entities
{
    public class InventoryTransaction : IEntity<long>
    {
        public long Id { get; set; }
        public long BookId { get; set; }
        public Book Book { get; set; }
        public int? Incremented { get; set; }
        public int? Decremented { get; set; }
        public int Stock { get; set; }
        public InventoryTransactionType Reason { get; set; }
        public string UserId { get; set; }        
        public DateTime ModifiedOn { get; set; }
    }
}
