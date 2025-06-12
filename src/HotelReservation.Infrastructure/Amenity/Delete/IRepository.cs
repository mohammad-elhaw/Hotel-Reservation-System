namespace HotelReservation.Infrastructure.Amenity.Delete;
public interface IRepository
{
    void Delete(Domain.Entities.Amenity amenity);
    Task<int> SaveChanges();
}
