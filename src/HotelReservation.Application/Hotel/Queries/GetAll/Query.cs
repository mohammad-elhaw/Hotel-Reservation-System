using HotelReservation.Domain;
using MediatR;

namespace HotelReservation.Application.Hotel.Queries.GetAll;
public record Query : IRequest<Result<List<Response>>>;
