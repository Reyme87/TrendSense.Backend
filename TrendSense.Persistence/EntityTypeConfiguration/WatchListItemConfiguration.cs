using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrendSense.Domain;

namespace TrendSense.Persistence.EntityTypeConfiguration
{
    public class WatchListItemConfiguration : IEntityTypeConfiguration<WatchListItem>
    {
        public void Configure(EntityTypeBuilder<WatchListItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Stock)
                .WithMany()
                .HasForeignKey(x => x.StockId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.AddedAt)
                .IsRequired();

            builder.HasIndex(x => new { x.WatchListId, x.StockId })
                .IsUnique();
        }
    }
}
