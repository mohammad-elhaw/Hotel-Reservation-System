using HotelReservation.Domain;

namespace HotelReservation.Queries.Room.GetById;
public interface IRepository
{
    Task<Result<Domain.Entities.Room>> GetById(Guid hotelId, Guid roomId);
}
