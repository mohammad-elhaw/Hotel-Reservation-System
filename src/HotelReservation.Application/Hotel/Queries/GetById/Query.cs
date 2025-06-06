using HotelReservation.Domain;
using MediatR;

namespace HotelReservation.Application.Hotel.Queries.GetById;
public record Query(Guid Id) : IRequest<Result<Response>>;

