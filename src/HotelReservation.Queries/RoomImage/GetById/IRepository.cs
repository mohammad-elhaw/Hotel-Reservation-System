using HotelReservation.Domain;

namespace HotelReservation.Queries.RoomImage.GetById;
public interface IRepository
{
    Task<Result<Domain.Entities.RoomImage>> GetById(Guid imageId);
}
