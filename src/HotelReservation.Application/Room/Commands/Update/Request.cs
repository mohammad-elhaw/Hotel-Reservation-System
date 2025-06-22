using HotelReservation.Domain;
using MediatR;

namespace HotelReservation.Application.Room.Commands.Update;
public record Request : IRequest<Result<Response>>
{
    public Guid HotelId { get; init; }
    public Guid RoomId { get; init; }
    public int RoomNumber { get; init; }
    public string Type { get; init; } = string.Empty;
    public bool IsAvailable { get; init; }
    public int Capacity { get; init; }
    public string Description { get; init; } = string.Empty;
    public double BasePricePerNight { get; init; }
}