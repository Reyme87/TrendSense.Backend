using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrendSense.Domain;

namespace TrendSense.Persistence.EntityTypeConfiguration
{
    public class WatchListConfiguration : IEntityTypeConfiguration<WatchList>
    {
        public void Configure(EntityTypeBuilder<WatchList> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Collection)
                .HasForeignKey(x => x.UserId);

            //builder.HasMany(x => x.Items)
            //    .WithOne()
            //    .HasForeignKey(x => x.WatchListId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
