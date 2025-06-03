using HotelReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelReservation.Infrastructure.Config;
internal class HotelConfig : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder.Property(h => h.Name)
            .HasMaxLength(100);

        builder.Property(h => h.Description)
            .HasMaxLength(500);

        builder.Property(h => h.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(h => h.Email)
            .HasMaxLength(100);

        builder.OwnsOne(h => h.Address, ad =>
        {
            ad.Property(a => a.Street)
                .HasColumnName("Street")
                .HasMaxLength(100);

            ad.Property(a => a.State)
                .HasColumnName("State")
                .HasMaxLength(100);

            ad.Property(a => a.City)
                .HasColumnName("City")
                .HasMaxLength(100);

            ad.Property(a => a.Country)
                .HasColumnName("Country")
                .HasMaxLength(100);

            ad.Property(a => a.ZipCode)
                .HasColumnName("ZipCode")
                .HasMaxLength(100);
        });


    }
}
