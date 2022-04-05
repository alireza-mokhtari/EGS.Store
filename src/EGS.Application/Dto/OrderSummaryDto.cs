using EGS.Domain.Entities;
using Mapster;

namespace EGS.Application.Dto
{
    public class OrderSummaryDto : IRegister
    {
        public OrderSummaryDto()
        {
        }

        public long Id { get; set; }
        public string CustomerId { get; set; }
        public string Comment { get; set; }
        public string OrderStatus { get; set; }
        public int ItemsCount { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Order, OrderSummaryDto>()
                .Map(dest => dest.OrderStatus, src => src.OrderHistories.Any()
                    ? src.OrderHistories.OrderByDescending(o => o.Id).FirstOrDefault().OrderStatus.ToString()
                    : Domain.Enums.OrderStatus.CheckedOut.ToString())
                .Map(dest => dest.ItemsCount, src => src.OrderItems.Count());
        }
    }
}
