using HotelReservation.Domain;
using MediatR;

namespace HotelReservation.Application.Reservation.Commands.Cancel;
public record Request(
    Guid ReservationId) : IRequest<Result>;
