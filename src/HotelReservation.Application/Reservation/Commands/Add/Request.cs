using HotelReservation.Domain;
using MediatR;

namespace HotelReservation.Application.Reservation.Commands.Add;
public record Request(
    DateTime CheckInDate,
    DateTime CheckOutDate,
    string CustomerName,
    string CustomerEmail,
    string CustomerPhoneNumber,
    Guid HotelId,
    List<Guid> RoomIds) : IRequest<Result<Response>>;
