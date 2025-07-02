using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Entities.Enums;

namespace HotelReservation.Application.Reservation.Commands.Add;
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
    HotelResponse Hotel);

public record HotelResponse(
    Guid Id,
    string Name,
    Address Address,
    string Description,
    double Rating);
