using HotelReservation.Domain.Entities;

namespace HotelReservation.Domain.Contracts;
public interface IReservationRepository
{
    void AddReservation(Reservation reservation);
}
