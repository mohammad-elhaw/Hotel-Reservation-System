using HotelReservation.Domain;
using MediatR;

namespace HotelReservation.Application.Amenity.Commands.Delete;
public record Request(Guid AmenityId) : IRequest<Result>;
