namespace HotelReservation.Infrastructure.RoomAmenity.Add;
public interface IRepository
{
    void Add(Domain.Entities.RoomAmenity roomAmenity);
    Task<int> SaveChanges();
}
