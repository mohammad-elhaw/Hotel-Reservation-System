using HotelReservation.Domain;
using MediatR;

namespace HotelReservation.Application.Amenity.Queries.GetAll;
public record Query() : IRequest<Result<List<Response>>>;
