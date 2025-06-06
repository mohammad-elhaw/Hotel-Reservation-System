using HotelReservation.Domain;
using MediatR;

namespace HotelReservation.Application.Hotel.Command.Delete;
public record Request(Guid Id) : IRequest<Result>;
