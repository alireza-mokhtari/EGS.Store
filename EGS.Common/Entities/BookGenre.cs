using EGS.Domain.Common;

namespace EGS.Domain.Entities
{
    public class BookGenre : IEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public IList<Book> Books { get; set; }
    }
}