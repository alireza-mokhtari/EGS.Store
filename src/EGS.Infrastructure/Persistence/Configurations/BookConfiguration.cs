using EGS.Domain.Entities;
using EGS.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace EGS.Infrastructure.Persistence.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Price)
                .IsPriceColumn();

            builder.Property(x => x.Modifier)
                .HasMaxLength(450);

            builder.Property(x => x.Creator)
                .HasMaxLength(450);
        }
    }
}
