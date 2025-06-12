using HotelReservation.Domain.Events;

namespace HotelReservation.Application.RoomImage.Events;
public record RoomImageDeleted(Guid ImageId, string Url) : IDomainEvent
{
    public DateTime OccurredOn => DateTime.UtcNow;
}
