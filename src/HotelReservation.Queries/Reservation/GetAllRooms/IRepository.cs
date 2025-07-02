using HotelReservation.Domain;

namespace HotelReservation.Queries.Reservation.GetAllRooms;
public interface IRepository
{
    public Task<Result<List<Domain.Entities.Room>>> GetAllRooms(Guid reservationId);
}
