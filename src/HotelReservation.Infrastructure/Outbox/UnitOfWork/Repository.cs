namespace HotelReservation.Infrastructure.Outbox.UnitOfWork;
public class Repository(
    HotelReservationDbContext context,
    GetPending.IRepository getPendingRepo,
    IncrementAttempts.IRepository incrementAttemptsRepo,
    MarkProcessed.IRepository markProcessedRepo) : IRepository
{
    public GetPending.IRepository GetPendingRepo => getPendingRepo;

    public IncrementAttempts.IRepository IncrementAttemptsRepo => incrementAttemptsRepo;

    public MarkProcessed.IRepository MarkProcessedRepo => markProcessedRepo;

    public Task<int> SaveChanges() => context.SaveChangesAsync();
}
