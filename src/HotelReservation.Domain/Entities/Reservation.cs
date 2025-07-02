using HotelReservation.Domain.Entities.Enums;
using Microsoft.AspNetCore.Http;

namespace HotelReservation.Domain.Entities;

public class Reservation : Entity
{
    // We need Customer property
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ExpirationTime { get; set; }
    public double TotalPrice { get; set; }
    public string CustomerName { get; set; } = null!;
    public string CustomerEmail { get; set; } = null!;
    public string CustomerPhoneNumber { get; set; } = null!;
    public BookingStatus Status { get; set; } = BookingStatus.Pending;

    // Navigation properties
    public Guid HotelId { get; set; }
    public Hotel Hotel { get; set; } = null!;
    public List<ReservationRoom> ReservationRooms { get; set; } = new();


    public record ReservationData(
        DateTime CheckInDate,
        DateTime CheckOutDate,
        double TotalPrice,
        string CustomerName,
        string CustomerEmail,
        string CustomerPhoneNumber,
        Guid HotelId
    );

    public static Result<Reservation> Create(ReservationData data)
    {
        if (data.CheckInDate >= data.CheckOutDate)
            return Result<Reservation>.Failure(
                ["Check-in date must be before check-out date."],
                StatusCodes.Status400BadRequest);
        var reservation = new Reservation
        {
            CheckInDate = data.CheckInDate,
            CheckOutDate = data.CheckOutDate,
            CreatedAt = DateTime.UtcNow,
            ExpirationTime = DateTime.UtcNow.AddHours(1),
            TotalPrice = data.TotalPrice,
            CustomerName = data.CustomerName,
            CustomerEmail = data.CustomerEmail,
            CustomerPhoneNumber = data.CustomerPhoneNumber,
            HotelId = data.HotelId
        };
        return Result<Reservation>.Success(reservation);
    }
}