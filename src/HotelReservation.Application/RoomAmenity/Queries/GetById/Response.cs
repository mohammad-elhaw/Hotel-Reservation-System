using HotelReservation.Domain.Entities.Enums;

namespace HotelReservation.Application.RoomAmenity.Queries.GetById;
public record Response(
    Guid Id,
    string Name,
    AmenityType Type);