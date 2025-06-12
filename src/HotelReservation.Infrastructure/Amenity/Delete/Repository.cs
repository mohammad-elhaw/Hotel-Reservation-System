namespace HotelReservation.Infrastructure.Amenity.Delete;
public class Repository(HotelReservationDbContext context) : IRepository
{
    public void Delete(Domain.Entities.Amenity amenity) => 
        context.Set<Domain.Entities.Amenity>().Remove(amenity);

    public async Task<int> SaveChanges() => await context.SaveChangesAsync();
}
