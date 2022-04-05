using EGS.Domain.Entities;
using Mapster;

namespace EGS.Application.Dto
{
    public class OrderItemDto : IRegister
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public long BookId { get; set; }
        public string BookTitle { get; set; }
        public string BookISBN { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<OrderItem, OrderItemDto>()
                .Map(dest => dest.BookTitle, src => src.Book != null ? src.Book.Title : "")
                .Map(dest => dest.BookISBN, src => src.Book != null ? src.Book.ISBN : "");
        }
    }
}