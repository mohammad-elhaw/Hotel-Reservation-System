namespace HotelReservation.Infrastructure.RoomAmenity.Add;
public class Repository(HotelReservationDbContext context) : IRepository
{
    public void Add(Domain.Entities.RoomAmenity roomAmenity) =>
        context.Set<Domain.Entities.RoomAmenity>().Add(roomAmenity);

    public async Task<int> SaveChanges() => await context.SaveChangesAsync();
}
