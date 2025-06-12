using HotelReservation.Domain.Events;
using System.Text.Json;

namespace HotelReservation.Domain.Entities;
public class OutboxMessage : Entity
{
    public DateTime OccurredOn { get; set; }
    public string Type { get; set; } = null!;
    public string Payload { get; set; } = null!;
    public int Attempts { get; set; }
    public bool IsProcessed { get; set; }

    public OutboxMessage() { }

    public OutboxMessage(IDomainEvent evt)
    {
        Type = evt.GetType().FullName!;
        Payload = JsonSerializer.Serialize(evt, evt.GetType());
    }
}
