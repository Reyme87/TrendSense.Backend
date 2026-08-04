using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrendSense.Domain;

namespace TrendSense.Persistence.EntityTypeConfiguration
{
    public class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.TickerSymbol)
                .HasMaxLength(4)
                .IsRequired();

            builder.HasIndex(x => x.TickerSymbol)
                .IsUnique();

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Exchange)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Currency)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.LastPrice)
                .HasPrecision(18, 4);

            builder.Property(x => x.DayChange)
                .HasPrecision(18, 4);

            builder.Property(x => x.DayChangePercent)
                .HasPrecision(5, 2);

            builder.Property(x => x.UpdatedAt)
                .IsRequired();
        }
    }
}
