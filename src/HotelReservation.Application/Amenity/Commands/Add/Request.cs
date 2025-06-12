using HotelReservation.Domain;
using MediatR;

namespace HotelReservation.Application.Amenity.Commands.Add;
public record Request(string Name, string Type) : IRequest<Result>;
