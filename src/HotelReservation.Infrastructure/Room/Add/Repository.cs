namespace HotelReservation.Infrastructure.Room.Add;
public class Repository(HotelReservationDbContext context) : IRepository
{
    public void Add(Domain.Entities.Room room) =>
        context.Set<Domain.Entities.Room>().Add(room);

    public async Task<int> SaveChanges() => await context.SaveChangesAsync();
}
