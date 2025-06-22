using HotelReservation.Domain;
using MediatR;

namespace HotelReservation.Application.Room.Commands.Add;
public record Request : IRequest<Result>
{
    public int RoomNumber { get; init; }
    public string Type { get; init; } = null!;
    public int Capacity { get; init; }
    public string Description { get; init; } = null!;
    public Guid HotelId { get; init; }
}