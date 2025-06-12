using HotelReservation.Application.Room.Queries.GetById;
using HotelReservation.Domain.Entities.Enums;

namespace HotelReservation.Application.Room.Queries.GetAll;
public record Response(
    Guid Id,
    int RoomNumber,
    RoomType Type,
    bool IsAvailable,
    int Capacity,
    string Description,
    Guid HotelId,
    List<RoomImageResponse> Images);