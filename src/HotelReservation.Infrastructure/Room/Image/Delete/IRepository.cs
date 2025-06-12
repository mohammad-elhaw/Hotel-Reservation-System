using HotelReservation.Domain.Entities;

namespace HotelReservation.Infrastructure.Room.Image.Delete;
public interface IRepository
{
    void Delete(RoomImage roomImage);
    Task<int> SaveChanges();
}
