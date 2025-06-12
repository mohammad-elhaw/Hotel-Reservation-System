using HotelReservation.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HotelReservation.Application.RoomImage.Command.Add;
public record Request(
    Guid HotelId,
    Guid RoomId,
    List<IFormFile> Images) : IRequest<Result>;

