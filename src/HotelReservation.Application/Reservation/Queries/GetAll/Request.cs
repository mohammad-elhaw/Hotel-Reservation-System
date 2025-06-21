using HotelReservation.Domain;
using MediatR;

namespace HotelReservation.Application.Reservation.Queries.GetAll;
public record Request(
    Guid HotelId): IRequest<Result<List<Response>>>;