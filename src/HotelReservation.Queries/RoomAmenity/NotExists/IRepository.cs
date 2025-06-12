using HotelReservation.Domain;

namespace HotelReservation.Queries.RoomAmenity.NotExists;
public interface IRepository
{
    Task<Result> NotExists(Guid roomId, Guid amenityId);
}
