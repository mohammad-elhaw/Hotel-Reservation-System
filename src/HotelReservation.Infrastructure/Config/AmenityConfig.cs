using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelReservation.Infrastructure.Config;
public class AmenityConfig : IEntityTypeConfiguration<Domain.Entities.Amenity>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Amenity> builder)
    {
        builder.ToTable("Amenities");
        builder.Property(a => a.Name)
            .HasMaxLength(100);

        builder.Property(a => a.Type)
            .HasConversion<string>();

        builder.HasIndex(a => a.Name).IsUnique();
        builder.HasIndex(a => a.Type);
    }
}
