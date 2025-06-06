namespace HotelReservation.Application.DTOs.Booking;
public record ReservationRequestDto
{
    public Guid HotelId { get; init; }
    public Guid RoomId { get; init; }
    public DateTime CheckInDate { get; init; }
    public DateTime CheckOutDate { get; init; }
    public string CustomerName { get; init; }
    public string CustomerEmail { get; init; }
    public string CustomerPhoneNumber { get; init; }

}

