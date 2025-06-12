using HotelReservation.Domain;
using MediatR;

namespace HotelReservation.Application.RoomAmenity.Commands.Delete;
public record Request(Guid RoomId, Guid AmenityId) : IRequest<Result>;
