using HotelReservation.Domain.Entities.Enums;

namespace HotelReservation.Application.Reservation.Queries.GetById;
public record Response(
    Guid Id,
    DateTime CheckInDate,
    DateTime CheckOutDate,
    DateTime CreatedAt,
    double TotalPrice,
    string CustomerName,
    string CustomerEmail,
    string CustomerPhoneNumber,
    BookingStatus Status,
    Hotel.Queries.GetById.Response Hotel);
