using System.ComponentModel.DataAnnotations;

namespace HotelReservation.Domain.Entities;

public class Amenity : Entity
{
    [MaxLength(100)]
    public string Name { get; set; } = null!;
    public List<RoomAmenity> RoomAmenities { get; set; } = new();
}