namespace HotelReservation.Infrastructure.Hotel.Delete;
public interface IRepository
{
    void DeleteHotel(Domain.Entities.Hotel hotel);
    Task<int> SaveChanges();
}
