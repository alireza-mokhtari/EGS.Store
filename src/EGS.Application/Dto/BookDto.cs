using EGS.Domain.Entities;
using Mapster;

namespace EGS.Application.Dto
{
    public class BookDto : IRegister
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int GenreId { get; set; }
        public string Genre { get; set; }
        public long Downloads { get; set; }
        public decimal Price { get; set; }
        public DateTime PublishDate { get; set; }
        public string PublishDateString { get; set; }
        public int Stock { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Book, BookDto>()
                .Map(dest => dest.Genre, src => src.Genre != null ? src.Genre.Title : "")
                .Map(dest => dest.PublishDateString, src => src.PublishDate.ToShortDateString())
                .Map(dest => dest.Stock, src => src.InventoryTransactions.Any()
                    ? src.InventoryTransactions.OrderByDescending(b => b.Id).FirstOrDefault().Stock
                    : 0)
                ;
        }
    }
}
