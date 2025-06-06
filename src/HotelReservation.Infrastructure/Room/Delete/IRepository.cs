namespace HotelReservation.Infrastructure.Room.Delete;
public interface IRepository
{
    void Delete(Domain.Entities.Room room);
    Task<int> SaveChanges();
}
