using HotelReservation.Domain.Entities.Enums;

namespace HotelReservation.Domain.Entities;

public class Reservation : Entity
{
    // We need Customer property
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public double TotalPrice { get; set; }
    public string CustomerName { get; set; } = null!;
    public string CustomerEmail { get; set; } = null!;
    public string CustomerPhoneNumber { get; set; } = null!;
    public BookingStatus Status { get; set; } = BookingStatus.Pending;

    // Navigation properties
    public Guid HotelId { get; set; }
    public Hotel Hotel { get; set; } = null!;
    public List<ReservationRoom> ReservationRooms { get; set; } = new();

}