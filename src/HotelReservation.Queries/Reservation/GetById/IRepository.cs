using HotelReservation.Domain;

namespace HotelReservation.Queries.Reservation.GetById;
public interface IRepository
{
    Task<Result<Domain.Entities.Reservation>> GetById(Guid reservationId);
}
