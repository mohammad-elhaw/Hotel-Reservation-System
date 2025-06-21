using HotelReservation.Domain;
using MediatR;

namespace HotelReservation.Application.Reservation.Queries.GetById;
public record Request(
    Guid HotelId,
    Guid ReservationId) : IRequest<Result<Response>>;