namespace HotelReservation.Domain.Entities;
public class Hotel : Entity
{
    public string Name { get; set; } = null!;
    public Address Address { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public double Rating { get; set; }

    public List<Room> Rooms { get; set; } = new ();
    public List<Reservation> Reservations { get; set; } = new ();
}
