namespace HotelReservation.Infrastructure.Outbox.IncrementAttempts;
public interface IRepository
{
    Task IncrementAttempts(Guid id);
}
