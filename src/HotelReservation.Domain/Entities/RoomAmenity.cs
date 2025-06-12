namespace HotelReservation.Domain.Entities;

public class RoomAmenity
{
    public Guid RoomId { get; set; }
    public Room Room { get; set; } = null!;

    public Guid AmenityId { get; set; }
    public Amenity Amenity { get; set; } = null!;

    public static Result<RoomAmenity> Create(Guid roomId, Guid amenityId)
    {
        var validationResult = ValidateRoomAmenity(roomId, amenityId);

        if(validationResult.IsFailure)
            return Result<RoomAmenity>.Failure(validationResult.Errors);

        var roomAmenity = new RoomAmenity
        {
            RoomId = roomId,
            AmenityId = amenityId
        };
        return Result<RoomAmenity>.Success(roomAmenity);
    }

    private static Result ValidateRoomAmenity(Guid roomId, Guid amenityId)
    {
        if (roomId == Guid.Empty)
            return Result.Failure(["Room ID cannot be empty."]);
        if (amenityId == Guid.Empty)
            return Result.Failure(["Amenity ID cannot be empty."]);

        return Result.Success();
    }
}