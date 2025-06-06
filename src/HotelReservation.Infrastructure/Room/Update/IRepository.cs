namespace HotelReservation.Infrastructure.Room.Update;
public interface IRepository
{
    void Update(Domain.Entities.Room room);
    Task<int> SaveChanges();
}
