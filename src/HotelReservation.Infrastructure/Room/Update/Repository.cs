namespace HotelReservation.Infrastructure.Room.Update;
public class Repository(HotelReservationDbContext context) : IRepository
{
    public void Update(Domain.Entities.Room room) =>
        context.Set<Domain.Entities.Room>().Update(room);

    public async Task<int> SaveChanges() => await context.SaveChangesAsync();
}
