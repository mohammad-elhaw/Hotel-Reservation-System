using HotelReservation.Domain.Entities;

namespace HotelReservation.Infrastructure.Room.Image.Add;
public class Repository(HotelReservationDbContext context) : IRepository
{
    public void Add(Guid RoomId, List<RoomImage> images) =>
        context.Set<RoomImage>().AddRange(images);

    public async Task<int> SaveChanges() => await context.SaveChangesAsync();
}
