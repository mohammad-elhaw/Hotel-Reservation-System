using HotelReservation.Domain.Contracts;

namespace HotelReservation.Infrastructure.Repositories;
public class RepositoryManager(
    IReservationRepository reservationRepo,
    HotelReservationDbContext context) : IRepositoryManager
{
    public IReservationRepository ReservationRepository => reservationRepo;

    public async Task<int> SaveChanges()=>
        await context.SaveChangesAsync();
}
