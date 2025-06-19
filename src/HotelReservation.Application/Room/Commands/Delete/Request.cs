using HotelReservation.Domain;
using MediatR;

namespace HotelReservation.Application.Room.Commands.Delete;
public record Request(
    Guid HotelId,
    Guid RoomId) : IRequest<Result>;
