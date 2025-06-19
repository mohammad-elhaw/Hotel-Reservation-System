using HotelReservation.Domain;

namespace HotelReservation.Infrastructure.UnitOfWork;
public interface IRepository
{
    Task<Result> SaveChanges();
}
