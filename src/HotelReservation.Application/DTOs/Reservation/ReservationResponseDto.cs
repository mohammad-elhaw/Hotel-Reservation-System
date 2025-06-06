using HotelReservation.Domain.Entities.Enums;

namespace HotelReservation.Application.DTOs.Booking;
public record ReservationResponseDto(
    Guid BookingId,
    string HotelName,
    RoomType? Type,
    DateTime CheckInDate,
    DateTime CheckOutDate,
    double TotalPrice,
    BookingStatus Status);
