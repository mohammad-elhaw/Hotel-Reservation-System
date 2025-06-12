using HotelReservation.Domain.Entities.Enums;

namespace HotelReservation.Application.Amenity.Queries.GetAll;
public record Response(
    Guid AmenityId,
    string Name,
    AmenityType Type
);