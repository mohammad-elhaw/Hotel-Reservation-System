using HotelReservation.Domain.Entities;

namespace HotelReservation.Infrastructure.Room.Image.Add;
public interface IRepository
{
    void Add(Guid RoomId, List<RoomImage> images);
    Task<int> SaveChanges();
}
