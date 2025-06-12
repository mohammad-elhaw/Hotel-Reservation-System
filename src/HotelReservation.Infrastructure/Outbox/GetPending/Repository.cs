using HotelReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelReservation.Infrastructure.Outbox.GetPending;
public class Repository(HotelReservationDbContext context) : IRepository
{
    public async Task<IReadOnlyList<OutboxMessage>> GetPending(int maxAttempts) =>
        await context.Set<OutboxMessage>()
        .Where(m => !m.IsProcessed && m.Attempts < maxAttempts)
        .ToListAsync();
}
