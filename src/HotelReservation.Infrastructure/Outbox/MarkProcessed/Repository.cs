using HotelReservation.Domain.Entities;

namespace HotelReservation.Infrastructure.Outbox.MarkProcessed;
public class Repository(HotelReservationDbContext context) : IRepository
{
    public async Task MarkProcessed(Guid outboxId)
    {
        var message = await context.Set<OutboxMessage>()
            .FindAsync(outboxId);

        if(message is not null)
            message.IsProcessed = true;
    }
}
