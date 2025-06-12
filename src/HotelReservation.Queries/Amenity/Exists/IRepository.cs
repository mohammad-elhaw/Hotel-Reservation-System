using HotelReservation.Domain;

namespace HotelReservation.Queries.Amenity.Exists;
public interface IRepository
{
    Task<Result> NotExists(string name);

    Task<Result> NotExists(string name, Guid amenityId);
    Task<Result> Exists(Guid amenityId);

}
