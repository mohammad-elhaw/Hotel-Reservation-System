using HotelReservation.Domain;

namespace HotelReservation.Infrastructure.Reservation.Delete;
public interface IRepository
{
    void Delete(Domain.Entities.Reservation reservation);
    Task<Result> SaveChanges();
}
