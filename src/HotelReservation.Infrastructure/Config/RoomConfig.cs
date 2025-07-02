using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelReservation.Infrastructure.Config;
internal class RoomConfig : IEntityTypeConfiguration<Domain.Entities.Room>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Room> builder)
    {
        builder.Property(r => r.Description)
            .HasMaxLength(500);

        builder.HasOne(r => r.Hotel)
            .WithMany(h => h.Rooms)
            .HasForeignKey(r => r.HotelId);

        builder.Property(r => r.Type)
            .HasConversion<string>();

        builder.HasIndex(r => r.IsAvailable);
    }
}
