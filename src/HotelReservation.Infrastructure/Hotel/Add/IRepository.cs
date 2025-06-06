namespace HotelReservation.Infrastructure.Hotel.Add;
public interface IRepository
{
    void AddHotel(Domain.Entities.Hotel hotel);
    Task<int> SaveChanges();
}
