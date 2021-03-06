using EGS.Domain.Entities;
using Mapster;

namespace EGS.Application.Dto
{
    public class OrderDto : IRegister
    {
        public OrderDto()
        {
            OrderItems = new List<OrderItemDto>();
        }

        public long Id { get; set; }
        public string CustomerId { get; set; }
        public string Comment { get; set; }
        public string OrderStatus { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Order, OrderDto>()
                .Map(dest => dest.OrderStatus, src => src.OrderHistories.Any()
                    ? src.OrderHistories.OrderByDescending(o => o.Id).FirstOrDefault().OrderStatus.ToString()
                    : Domain.Enums.OrderStatus.CheckedOut.ToString())
                ;
        }
    }

    public class OrderHistoryItemDto : IRegister
    {
        public long OrderId { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }
        public string OccuredAt { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<OrderHistory, OrderHistoryItemDto>()
                .Map(dest => dest.Status, src => src.OrderStatus.ToString())
                .Map(dest => dest.OccuredAt, src => $"{src.OccuredAt.ToShortDateString()} {src.OccuredAt.ToShortTimeString()}");
        }
    }
}
