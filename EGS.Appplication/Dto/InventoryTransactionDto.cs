using EGS.Domain.Entities;
using Mapster;

namespace EGS.Application.Dto
{
    public class InventoryTransactionDto : IRegister
    {
        public long BookId { get; set; }
        public string ModifiedOn { get; set; }
        public int Incremented { get; set; }
        public int Decremented { get; set; }
        public int Stock { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<InventoryTransaction, InventoryTransactionDto>()
                .Map(dest => dest.ModifiedOn, src => $"{src.ModifiedOn.ToShortDateString()} {src.ModifiedOn.ToShortTimeString()}");
        }
    }
}
