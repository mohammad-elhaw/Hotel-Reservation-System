using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelReservation.Domain.Entities;

public class RoomImage : Entity
{
    [ForeignKey(nameof(RoomId))]
    public Guid RoomId { get; set; }
    public Room Room { get; set; } = null!;
    [MaxLength(300)]
    public string ImageUrl { get; set; } = null!;
}