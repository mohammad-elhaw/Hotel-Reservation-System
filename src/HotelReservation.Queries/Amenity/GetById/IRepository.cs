using HotelReservation.Domain;

namespace HotelReservation.Queries.Amenity.GetById;
public interface IRepository
{
    Task<Result<Domain.Entities.Amenity>> GetById(Guid amenityId);
}
