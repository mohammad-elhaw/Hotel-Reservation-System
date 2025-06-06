using HotelReservation.Domain;
using HotelReservation.Domain.Entities.Enums;
using MediatR;

namespace HotelReservation.Application.Room.Commands.Add;
public record Request : IRequest<Result>
{
    public int RoomNumber { get; init; }
    public RoomType Type { get; init; }
    public int Capacity { get; init; }
    public string Description { get; init; } = null!;
    public Guid HotelId { get; init; }
}