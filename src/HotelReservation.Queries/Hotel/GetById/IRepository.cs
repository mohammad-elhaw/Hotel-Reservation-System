using HotelReservation.Domain;

namespace HotelReservation.Queries.Hotel.GetById;
public interface IRepository
{
    Task<Result<Domain.Entities.Hotel>> GetById(Guid id);
}
