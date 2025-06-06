namespace HotelReservation.Queries.Hotel;
public sealed record HotelDbModel
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string Street { get; init; } = null!;
    public string City { get; init; } = null!;
    public string State { get; init; } = null!;
    public string ZipCode { get; init; } = null!;
    public string Country { get; init; } = null!;
    public string Description { get; init; } = null!;
    public string PhoneNumber { get; init; } = null!;
    public string Email { get; init; } = null!;
    public double Rating { get; init; } = default!;
}
