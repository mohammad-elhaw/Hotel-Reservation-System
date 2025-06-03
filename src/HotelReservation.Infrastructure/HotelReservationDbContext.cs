using Microsoft.EntityFrameworkCore;

namespace HotelReservation.Infrastructure;

public class HotelReservationDbContext(DbContextOptions<HotelReservationDbContext> options) : 
    DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HotelReservationDbContext).Assembly);
    }
}
