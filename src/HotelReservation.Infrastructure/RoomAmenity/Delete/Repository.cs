namespace HotelReservation.Infrastructure.RoomAmenity.Delete;
public class Repository(HotelReservationDbContext context) : IRepository
{
    public void Delete(Domain.Entities.RoomAmenity roomAmenity) =>
        context.Set<Domain.Entities.RoomAmenity>().Remove(roomAmenity);

    public async Task<int> SaveChanges() => await context.SaveChangesAsync();
}
