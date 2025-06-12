using HotelReservation.Domain;
using HotelReservation.Domain.Entities.Enums;
using MediatR;

namespace HotelReservation.Application.Amenity.Commands.Update;
public record Request(
    string Name,
    string Type) : IRequest<Result>
{
    public Guid AmenityId { get; init; }
}
