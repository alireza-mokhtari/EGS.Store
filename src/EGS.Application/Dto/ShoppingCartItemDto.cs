using EGS.Domain.Entities;
using Mapster;

namespace EGS.Application.Dto
{
    public class ShoppingCartItemDto : IRegister
    {
        public long BookId { get; set; }
        public string BookTitle { get; set; }
        public string BookAuthor { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total => Price * Quantity;
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ShoppingCartItem, ShoppingCartItemDto>()
                .Map(dest => dest.BookTitle, src => src.Book != null ? src.Book.Title : null)
                .Map(dest => dest.BookAuthor, src => src.Book != null ? src.Book.Author : null);

            config.NewConfig<ShoppingCartItemDto, OrderItem>();
        }
    }
}
