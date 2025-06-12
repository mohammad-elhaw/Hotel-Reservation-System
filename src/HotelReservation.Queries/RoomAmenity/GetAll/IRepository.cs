using HotelReservation.Domain;

namespace HotelReservation.Queries.RoomAmenity.GetAll;
public interface IRepository
{
    Task<Result<List<Domain.Entities.Amenity>>> GetAll(Guid roomId);
}
