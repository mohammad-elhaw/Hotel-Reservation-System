using HotelReservation.Domain;

namespace HotelReservation.Queries.Hotel.Exists;
public interface IRepository
{
    Task<Result<bool>> Exists(Guid hotelId);
}
