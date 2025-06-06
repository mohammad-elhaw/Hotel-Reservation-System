using HotelReservation.Domain;
using HotelReservation.Domain.Entities;
using MediatR;

namespace HotelReservation.Application.Hotel.Command.Update;
public record Request (
    Guid Id,
    string Name,
    AddressData Address,
    string Description,
    string PhoneNumber,
    string Email,
    double Rating) : IRequest<Result<Domain.Entities.Hotel>>;
