using HotelReservation.Domain;
using MediatR;

namespace HotelReservation.Application.Room.Queries.GetById;
public record Query(Guid HotelId, Guid RoomId) : IRequest<Result<Response>>;
