using HotelReservation.Domain;
using HotelReservation.Domain.Entities.Enums;

namespace HotelReservation.Queries.Reservation.GetAll;
public interface IRepository
{
    Task<Dictionary<Guid, List<(DateTime CheckIn, DateTime CheckOut,
        BookingStatus Status)>>> GetValidReservationsForRooms(List<Guid> roomIds);

    Task<Result<List<Domain.Entities.Reservation>>> GetAllReservations(Guid hotelId);
}
