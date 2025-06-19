namespace HotelReservation.Infrastructure.Reservation.Add;
public class Repository(HotelReservationDbContext context) : IRepository
{
    public void Add(Domain.Entities.Reservation reservation) =>
        context.Set<Domain.Entities.Reservation>().Add(reservation);

    public async Task<int> SaveChanges() => await context.SaveChangesAsync();
}
