using HotelReservation.Domain.Entities;

namespace HotelReservation.Application.Hotel.Queries.GetAll;
public record Response(
    Guid Id,
    string Name,
    Address Address,
    string Description,
    double Rating);
