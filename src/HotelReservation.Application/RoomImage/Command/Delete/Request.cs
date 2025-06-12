using HotelReservation.Domain;
using MediatR;

namespace HotelReservation.Application.RoomImage.Command.Delete;
public record Request(Guid ImageId) : IRequest<Result>;