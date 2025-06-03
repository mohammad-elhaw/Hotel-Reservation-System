using HotelReservation.Domain.Entities.Enums;

namespace HotelReservation.Domain.Entities;
public class Room : Entity
{
    public int RoomNumber { get; set; }
    public RoomType Type { get; set; }
    public bool IsAvailable { get; set; } = true;
    public int Capacity { get; set; }
    public string Description { get; set; } = null!;
    // Navigation properties
    public Guid HotelId { get; set; }
    public Hotel Hotel { get; set; } = null!;
    public List<RoomImage> Images { get; set; } = new();
    public List<ReservationRoom> ReservationRooms { get; set; } = new();
    public List<RoomAmenity> RoomAmenities { get; set; } = new();
}
