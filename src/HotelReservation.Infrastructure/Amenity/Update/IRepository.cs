namespace HotelReservation.Infrastructure.Amenity.Update;
public interface IRepository
{
    void Update(Domain.Entities.Amenity amenity);
    Task<int> SaveChanges();
}
