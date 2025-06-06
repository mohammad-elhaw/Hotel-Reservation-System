namespace HotelReservation.Domain.Contracts;
public interface IRepositoryManager
{
    public IReservationRepository ReservationRepository { get; }
    public Task<int> SaveChanges();
}
