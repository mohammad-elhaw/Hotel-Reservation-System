using HotelReservation.Domain.Entities;

namespace HotelReservation.Infrastructure.Outbox.IncrementAttempts;
public class Repository(HotelReservationDbContext context) : IRepository
{
    public async Task IncrementAttempts(Guid id)
    {
        var message = await context.Set<OutboxMessage>()
            .FindAsync(id);

        if (message is not null) message.Attempts++;
    }
}
