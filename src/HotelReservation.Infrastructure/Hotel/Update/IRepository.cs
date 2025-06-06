namespace HotelReservation.Infrastructure.Hotel.Update;
public interface IRepository
{
    void UpdateHotel(Domain.Entities.Hotel hotel);
    Task<int> SaveChanges();
}
