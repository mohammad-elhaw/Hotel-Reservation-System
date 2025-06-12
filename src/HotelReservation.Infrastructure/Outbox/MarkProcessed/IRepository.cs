namespace HotelReservation.Infrastructure.Outbox.MarkProcessed;
public interface IRepository
{
    Task MarkProcessed(Guid outboxId);
}
