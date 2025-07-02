using HotelReservation.Domain;

namespace HotelReservation.Queries.Room.CleanUpRooms;
public interface IRepository
{
    Task<Result> CleanupRoomAvailability();
}
