using HotelReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelReservation.Infrastructure.Config;
public class OutboxMessageConfig : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("OutboxMessages");
        builder.HasIndex(x => new { x.IsProcessed, x.Attempts });
        builder.Property(om => om.Type)
            .HasMaxLength(250);
        builder.Property(om => om.Payload)
            .HasMaxLength(1000);
    }
}
