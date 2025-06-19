namespace HotelReservation.Infrastructure.Reservation.Add;
public interface IRepository
{
    void Add(Domain.Entities.Reservation reservation);

    Task<int> SaveChanges();
}
