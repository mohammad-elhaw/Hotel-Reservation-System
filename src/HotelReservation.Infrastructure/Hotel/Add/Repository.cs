namespace HotelReservation.Infrastructure.Hotel.Add;
public class Repository(HotelReservationDbContext context) : IRepository
{
    public void AddHotel(Domain.Entities.Hotel hotel)
    {
        context.Set<Domain.Entities.Hotel>().Add(hotel);
    }

    public async Task<int> SaveChanges()
    {
        return await context.SaveChangesAsync();    
    }
}
