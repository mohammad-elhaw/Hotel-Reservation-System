namespace HotelReservation.Infrastructure.RoomAmenity.Delete;
public interface IRepository
{
    void Delete(Domain.Entities.RoomAmenity roomAmenity);
    Task<int> SaveChanges();
}
