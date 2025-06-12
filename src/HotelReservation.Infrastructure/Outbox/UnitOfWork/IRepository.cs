namespace HotelReservation.Infrastructure.Outbox.UnitOfWork;
public interface IRepository
{
    GetPending.IRepository GetPendingRepo { get; }
    IncrementAttempts.IRepository IncrementAttemptsRepo { get; }
    MarkProcessed.IRepository MarkProcessedRepo { get; }
    Task<int> SaveChanges();
}
