using HotelReservation.Domain.Entities;

namespace HotelReservation.Infrastructure.Outbox.GetPending;
public interface IRepository
{
    Task<IReadOnlyList<OutboxMessage>> GetPending(int maxAttempts);
}
