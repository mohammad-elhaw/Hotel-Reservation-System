using HotelReservation.Domain;

namespace HotelReservation.Queries.Hotel.GetAll;
public interface IRepository
{
    Task<Result<List<Domain.Entities.Hotel>>> GetAll();
}
