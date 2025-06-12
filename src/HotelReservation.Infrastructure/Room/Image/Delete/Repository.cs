
using HotelReservation.Domain.Entities;

namespace HotelReservation.Infrastructure.Room.Image.Delete;
public class Repository(HotelReservationDbContext context) : IRepository
{
    public void Delete(RoomImage roomImage) =>
        context.Set<RoomImage>().Remove(roomImage);

    public async Task<int> SaveChanges() => await context.SaveChangesAsync();
}
