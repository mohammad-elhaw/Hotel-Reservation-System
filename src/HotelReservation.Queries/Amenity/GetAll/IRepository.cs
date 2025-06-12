using HotelReservation.Domain;

namespace HotelReservation.Queries.Amenity.GetAll;
public interface IRepository
{
    Task<Result<List<Domain.Entities.Amenity>>> GetAll();
}
