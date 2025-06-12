namespace HotelReservation.Infrastructure.Amenity.Add;
public class Repository(HotelReservationDbContext context) : IRepository
{
    public void Add(Domain.Entities.Amenity amenity) => 
        context.Set<Domain.Entities.Amenity>().Add(amenity);

    public async Task<int> SaveChanges() => await context.SaveChangesAsync();
}
