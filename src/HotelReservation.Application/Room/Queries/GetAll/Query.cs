using HotelReservation.Domain;
using MediatR;

namespace HotelReservation.Application.Room.Queries.GetAll;
public record Query(Guid HotelId) : IRequest<Result<List<Response>>>;
