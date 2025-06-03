namespace HotelReservation.Domain.Entities;

public class ReservationRoom
{
    public Guid ReservationId { get; set; }
    public Guid RoomId { get; set; }
    public double PricePerNight { get; set; }

    public Reservation Reservation { get; set; } = null!;
    public Room Room { get; set; } = null!;
}