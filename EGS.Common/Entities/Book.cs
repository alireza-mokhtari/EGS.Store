using EGS.Domain.Common;

namespace EGS.Domain.Entities
{
    public class Book : AuditableEntity , IEntity<long>
    {
        public Book()
        {
            InventoryTransactions = new List<InventoryTransaction>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int GenreId { get; set; }
        public BookGenre Genre { get; set; }
        public long TotalSold { get; set; }
        public decimal Price { get; set; }
        public DateTime PublishDate { get; set; }        
        public IList<InventoryTransaction> InventoryTransactions { get; set; }
    }

}
