namespace HotelReservation.Infrastructure.Amenity.Add;
public interface IRepository
{
    void Add(Domain.Entities.Amenity amenity);
    Task<int> SaveChanges();
}
