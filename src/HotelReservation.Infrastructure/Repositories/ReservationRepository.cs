using HotelReservation.Domain.Contracts;
using HotelReservation.Domain.Entities;

namespace HotelReservation.Infrastructure.Repositories;
public class ReservationRepository(HotelReservationDbContext context) : IReservationRepository
{
    public void AddReservation(Reservation reservation) =>
        context.Set<Reservation>().Add(reservation);
    


}
