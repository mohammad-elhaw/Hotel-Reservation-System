using HotelReservation.Domain;

namespace HotelReservation.Infrastructure.Reservation.Cancel;
public interface IRepository
{
    void CancelReservation(Domain.Entities.Reservation reservation);
    Task<int> SaveChanges();
}
