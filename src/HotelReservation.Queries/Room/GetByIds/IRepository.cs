using HotelReservation.Domain;

namespace HotelReservation.Queries.Room.GetByIds;
public interface IRepository
{
    Task<Result<List<Domain.Entities.Room>>> GetByIds(List<Guid> roomIds, Guid hotelId);
}
