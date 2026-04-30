using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrendSense.Domain;

namespace TrendSense.Persistence.EntityTypeConfiguration
{
    public class PriceHistoryConfiguration : IEntityTypeConfiguration<PriceHistory>
    {
        public void Configure(EntityTypeBuilder<PriceHistory> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Stock)
                .WithMany()
                .HasForeignKey(x => x.StockId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Price)
                .HasPrecision(18, 4);

            builder.Property(x => x.RecordedAt)
                .IsRequired();

            builder.HasIndex(x => new { x.StockId, x.RecordedAt });
        }
    }
}
