using EGS.Domain.Entities;
using EGS.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EGS.Infrastructure.Persistence.Configurations
{
    public class ShoppingCartItemConfiguration : IEntityTypeConfiguration<ShoppingCartItem>
    {
        public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Price)
                .IsPriceColumn();
            
            builder.Property(x => x.CustomerId)
                .IsUserColumn();
        }
    }

    public class ShoppingCartHistoryConfiguration : IEntityTypeConfiguration<ShoppingCartHistory>
    {
        public void Configure(EntityTypeBuilder<ShoppingCartHistory> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CustomerId)
                .IsUserColumn();
        }
    }
}
