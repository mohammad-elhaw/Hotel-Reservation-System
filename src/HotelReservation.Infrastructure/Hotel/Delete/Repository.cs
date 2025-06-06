namespace HotelReservation.Infrastructure.Hotel.Delete;
public class Repository(HotelReservationDbContext context) : IRepository
{
    public void DeleteHotel(Domain.Entities.Hotel hotel) =>
        context.Set<Domain.Entities.Hotel>().Remove(hotel);
    
    public async Task<int> SaveChanges() => await context.SaveChangesAsync();
}
