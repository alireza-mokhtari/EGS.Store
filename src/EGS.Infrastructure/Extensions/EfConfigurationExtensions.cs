using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EGS.Infrastructure.Extensions
{
    public static class EfConfigurationExtensions
    {
        public static PropertyBuilder<decimal> IsPriceColumn(this PropertyBuilder<decimal> propertyBuilder)
        {
            return propertyBuilder.HasColumnType("decimal(15,2)");
        }

        public static PropertyBuilder<string> IsUserColumn(this PropertyBuilder<string> propertyBuilder)
        {
            return propertyBuilder.HasMaxLength(450);
        }
    }
}
