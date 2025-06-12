using HotelReservation.Domain;
using MediatR;

namespace HotelReservation.Application.RoomAmenity.Queries.GetById;
public record Query(
    Guid RoomId,
    Guid AmenityId) : IRequest<Result<Response>>;