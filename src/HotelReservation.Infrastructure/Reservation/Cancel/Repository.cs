namespace HotelReservation.Infrastructure.Reservation.Cancel;
public class Repository(HotelReservationDbContext context) : IRepository
{
    public void CancelReservation(Domain.Entities.Reservation reservation) =>
        context.Set<Domain.Entities.Reservation>().Update(reservation);
    
    public async Task<int> SaveChanges() =>
        await context.SaveChangesAsync();
}
