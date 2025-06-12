using HotelReservation.Domain;
using MediatR;

namespace HotelReservation.Application.RoomAmenity.Queries.GetAll;
public record Query(Guid RoomId) : IRequest<Result<List<Response>>>;