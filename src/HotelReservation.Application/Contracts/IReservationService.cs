using HotelReservation.Application.DTOs.Booking;

namespace HotelReservation.Application.Contracts;
public interface IReservationService
{
    Task<ReservationResponseDto> CreateReservation(ReservationRequestDto reservationRequest);
}
