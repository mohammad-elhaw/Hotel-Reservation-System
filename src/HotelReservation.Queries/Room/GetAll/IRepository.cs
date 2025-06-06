using HotelReservation.Domain;

namespace HotelReservation.Queries.Room.GetAll;
public interface IRepository
{
    Task<Result<List<Domain.Entities.Room>>> GetAll(Guid hotelId);
}
