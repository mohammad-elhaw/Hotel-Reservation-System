namespace HotelReservation.Infrastructure.Hotel.Update;
public class Repository(HotelReservationDbContext context) : IRepository
{
    public void UpdateHotel(Domain.Entities.Hotel hotel)
    {
        context.Set<Domain.Entities.Hotel>().Update(hotel);
    }

    public async Task<int> SaveChanges()
    {
        return await context.SaveChangesAsync();
    }
}
