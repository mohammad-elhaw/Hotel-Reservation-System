using HotelReservation.Domain.Events;
using Microsoft.AspNetCore.Http;

namespace HotelReservation.Application.RoomImage.Events;
public record RoomImageAdded(IFormFile image, string Url) : IDomainEvent
{
    public DateTime OccurredOn => DateTime.UtcNow;
}
