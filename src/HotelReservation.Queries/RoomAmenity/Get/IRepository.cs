using HotelReservation.Domain;

namespace HotelReservation.Queries.RoomAmenity.Get;
public interface IRepository
{
    Task<Result<Domain.Entities.RoomAmenity>> GetRoomAmenity(Guid roomId, Guid amenityId);
}
