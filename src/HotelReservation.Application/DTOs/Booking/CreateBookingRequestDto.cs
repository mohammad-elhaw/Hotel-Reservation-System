namespace HotelReservation.Application.DTOs.Booking;
public record CreateBookingRequestDto(
    Guid HotelId,
    Guid RoomId,
    DateTime CheckInDate,
    DateTime CheckOutDate,
    string GuestName,
    string GuestEmail,
    string GuestPhoneNumber
);
