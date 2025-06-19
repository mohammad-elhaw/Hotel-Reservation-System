using HotelReservation.Domain;
using MediatR;

namespace HotelReservation.Application.Reservation.Commands.Delete;
public record Request(
    Guid ReservationId) : IRequest<Result>;
