using HotelReservation.Domain.Entities;

namespace HotelReservation.Infrastructure.Outbox.Add;
public interface IRepository
{
    void Add(OutboxMessage message);
    Task<int> SaveChanges();
}
