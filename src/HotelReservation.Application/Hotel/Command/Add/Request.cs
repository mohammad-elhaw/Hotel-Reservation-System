using HotelReservation.Domain;
using HotelReservation.Domain.Entities;
using MediatR;

namespace HotelReservation.Application.Hotel.Command.Add;
public record Request(
    string Name,
    AddressData Address,
    string Description,
    string PhoneNumber,
    string Email,
    double Rating) : IRequest<Result<Response>>;
