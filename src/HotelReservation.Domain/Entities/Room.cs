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

    public record RoomData(
        int RoomNumber,
        RoomType Type,
        int Capacity,
        string Description,
        Guid HotelId);

    public static Result<Room> Create(RoomData data)
    {
        var validationResult = ValidateRoom(data);
        if(validationResult.IsFailure)
            return Result<Room>.Failure(validationResult.Errors);

        var room = new Room()
        {
            RoomNumber = data.RoomNumber,
            Type = data.Type,
            Capacity = data.Capacity,
            Description = data.Description,
            HotelId = data.HotelId,
            IsAvailable = true
        };

        return Result<Room>.Success(room);
    }

    public static Result<Room> Update(Room room, RoomData newData)
    {
        var validationResult = ValidateRoom(newData);
        if (validationResult.IsFailure)
            return Result<Room>.Failure(validationResult.Errors);

        room.RoomNumber = newData.RoomNumber;
        room.Type = newData.Type;
        room.Capacity = newData.Capacity;
        room.Description = newData.Description;
        room.HotelId = newData.HotelId;
        return Result<Room>.Success(room);
    }

    private static Result<RoomData> ValidateRoom(RoomData data)
    {
        List<string> errors = new();

        if (data.RoomNumber <= 0)
            errors.Add("Room number must be greater than 0");

        if (!Enum.IsDefined(data.Type))
            errors.Add("Invalid RoomType");

        if (data.Capacity <= 0)
            errors.Add("Capacity must be greater than 0");

        if (string.IsNullOrWhiteSpace(data.Description))
            errors.Add("Description is required");

        if (data.HotelId == Guid.Empty)
            errors.Add("Hotel ID is required");

        if (errors.Any())
            return Result<RoomData>.Failure(errors);

        return Result<RoomData>.Success(data);
    }
}
