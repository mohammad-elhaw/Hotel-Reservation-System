using HotelReservation.Domain;

namespace HotelReservation.Queries.RoomAmenity.GetById;
public interface IRepository
{
    Task<Result<Domain.Entities.Amenity>> GetById(Guid roomId, Guid amenityId);
}
