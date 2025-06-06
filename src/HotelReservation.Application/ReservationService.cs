using HotelReservation.Application.Contracts;
using HotelReservation.Application.DTOs.Booking;
using HotelReservation.Domain.Contracts;
using HotelReservation.Domain.Entities;

namespace HotelReservation.Application;
public class ReservationService(IRepositoryManager repositoryManager) : IReservationService
{
    public async Task<ReservationResponseDto> CreateReservation(ReservationRequestDto reservationRequest)
    {
        var totalDays = (reservationRequest.CheckOutDate - reservationRequest.CheckInDate).Days;
        //var totalPrice = totalDays * bookingRequest.

        var reservationEntity = new Reservation()
        {
            CheckInDate = reservationRequest.CheckInDate,
            CheckOutDate = reservationRequest.CheckOutDate,
            CreatedAt = DateTime.UtcNow,
            CustomerEmail = reservationRequest.CustomerEmail,
            CustomerName = reservationRequest.CustomerName,
            CustomerPhoneNumber = reservationRequest.CustomerPhoneNumber,
            HotelId = reservationRequest.HotelId
        };

        repositoryManager.ReservationRepository.AddReservation(reservationEntity);
        await repositoryManager.SaveChanges();

        return new ReservationResponseDto
        (
            BookingId: reservationEntity.Id,
            HotelName: reservationEntity.Hotel?.Name,
            Type: reservationEntity.ReservationRooms?.
                Where(r => r.RoomId == reservationRequest.RoomId)
                .FirstOrDefault().Room.Type,
            CheckInDate: reservationEntity.CheckInDate,
            CheckOutDate: reservationEntity.CheckOutDate,
            TotalPrice: reservationEntity.TotalPrice,
            Status: reservationEntity.Status
        );
    }
}
