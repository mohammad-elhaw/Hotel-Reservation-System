using HotelReservation.Domain.Entities;

namespace HotelReservation.Application.Hotel.Queries.GetById;
public record Response(
    Guid Id,
    string Name,
    Address Address,
    string Description,
    double Rating
);
