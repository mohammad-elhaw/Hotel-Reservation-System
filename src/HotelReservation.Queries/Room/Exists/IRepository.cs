using HotelReservation.Domain;

namespace HotelReservation.Queries.Room.Exists;
public interface IRepository
{
    Task<Result> Exists(Guid roomId);
}
