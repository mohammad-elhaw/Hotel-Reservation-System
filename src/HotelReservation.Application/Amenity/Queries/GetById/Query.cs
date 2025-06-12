using HotelReservation.Domain;
using MediatR;

namespace HotelReservation.Application.Amenity.Queries.GetById;
public record Query(Guid AmenityId) : IRequest<Result<Response>>;