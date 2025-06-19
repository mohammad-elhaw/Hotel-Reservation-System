using HotelReservation.Domain.Entities.Enums;

namespace HotelReservation.Queries.Reservation.GetAll;
public interface IRepository
{
    Task<Dictionary<Guid, List<(DateTime CheckIn, DateTime CheckOut,
        BookingStatus Status)>>> GetReservationsForRooms(List<Guid> roomIds);
}
