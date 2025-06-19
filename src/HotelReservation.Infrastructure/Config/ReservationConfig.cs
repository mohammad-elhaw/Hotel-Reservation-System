using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelReservation.Infrastructure.Config;
internal class ReservationConfig : IEntityTypeConfiguration<Domain.Entities.Reservation>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Reservation> builder)
    {
        builder.Property(r => r.CreatedAt)
            .HasDefaultValueSql("GETDATE()");

        builder.Property(r => r.TotalPrice)
            .HasColumnType("decimal(18,2)");

        builder.Property(r => r.CustomerName)
            .HasMaxLength(100);
        
        builder.Property(r => r.CustomerEmail)
            .HasMaxLength(100);
        
        builder.Property(r => r.CustomerPhoneNumber)
            .HasMaxLength(15);

        builder.Property(r => r.Status)
            .HasConversion<string>();

        builder.HasOne(r => r.Hotel)
            .WithMany(h => h.Reservations)
            .HasForeignKey(r => r.HotelId);
    }
}
