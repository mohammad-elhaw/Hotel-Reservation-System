using HotelReservation.Domain.Entities;

namespace HotelReservation.Application.Hotel.Command.Add;
public record Response(
    Guid Id,
    string Name,
    Address Address,
    string Description,
    string PhoneNumber,
    string Email,
    double Rating
);
