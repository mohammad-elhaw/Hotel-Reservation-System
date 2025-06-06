namespace HotelReservation.Infrastructure.Room.Delete;
public class Repository(HotelReservationDbContext context) : IRepository
{
    public void Delete(Domain.Entities.Room room) =>
        context.Set<Domain.Entities.Room>().Remove(room);

    public async Task<int> SaveChanges() => await context.SaveChangesAsync();
    
}
