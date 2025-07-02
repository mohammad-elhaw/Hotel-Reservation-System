using HotelReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelReservation.Infrastructure.Config;
internal class ReservationRoomConfig : IEntityTypeConfiguration<ReservationRoom>
{
    public void Configure(EntityTypeBuilder<ReservationRoom> builder)
    {
        builder.HasKey(rr => new { rr.ReservationId, rr.RoomId });

        builder.HasIndex(rr => rr.RoomId);

        builder.Property(rr => rr.PricePerNight)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.HasOne(rr => rr.Reservation)
            .WithMany(r => r.ReservationRooms)
            .HasForeignKey(rr => rr.ReservationId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasOne(rr => rr.Room)
            .WithMany(r => r.ReservationRooms)
            .HasForeignKey(rr => rr.RoomId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
