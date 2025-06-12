namespace HotelReservation.Infrastructure.Amenity.Update;
public class Repository(HotelReservationDbContext context) : IRepository
{
    public void Update(Domain.Entities.Amenity amenity) =>
        context.Set<Domain.Entities.Amenity>().Update(amenity);

    public async Task<int> SaveChanges() => await context.SaveChangesAsync();

}
