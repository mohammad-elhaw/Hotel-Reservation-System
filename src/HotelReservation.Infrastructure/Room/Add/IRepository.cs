namespace HotelReservation.Infrastructure.Room.Add;
public interface IRepository
{
    void Add(Domain.Entities.Room room);
    Task<int> SaveChanges();
}
