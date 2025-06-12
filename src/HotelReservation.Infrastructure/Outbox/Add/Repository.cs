using HotelReservation.Domain.Entities;

namespace HotelReservation.Infrastructure.Outbox.Add;
public class Repository(HotelReservationDbContext context) : IRepository
{
    public void Add(OutboxMessage message) =>
        context.Set<OutboxMessage>().Add(message);

    public async Task<int> SaveChanges() => await context.SaveChangesAsync();
}
