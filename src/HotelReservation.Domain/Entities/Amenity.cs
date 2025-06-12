using HotelReservation.Domain.Entities.Enums;

namespace HotelReservation.Domain.Entities;

public class Amenity : Entity
{
    public string Name { get; private set; } = null!;
    public AmenityType Type { get; private set; }
    public List<RoomAmenity> RoomAmenities { get; set; } = new();


    public record AmenityData(string Name, string Type);
    private sealed record ValidatedData(string Name, AmenityType Type);

    public static Result<Amenity> Create(AmenityData amenityData)
    {
        var validationResult = ValidateAmenity(amenityData);
        if (validationResult.IsFailure)
            return Result<Amenity>.Failure(validationResult.Errors);

        Amenity amenity = new()
        {
            Name = validationResult.Value!.Name,
            Type = validationResult.Value.Type
        };
        return Result<Amenity>.Success(amenity);
    }

    public static Result Update(Amenity amenityToUpdate, AmenityData amenityData)
    {
        var validationResult = ValidateAmenity(amenityData);
        if (validationResult.IsFailure)
            return Result.Failure(validationResult.Errors);

        amenityToUpdate.Name = validationResult.Value!.Name;
        amenityToUpdate.Type = validationResult.Value.Type;

        return Result.Success();
    }

    private static Result<ValidatedData> ValidateAmenity(AmenityData amenityData)
    {
        if (string.IsNullOrWhiteSpace(amenityData.Name))
            return Result<ValidatedData>.Failure(
                ["Amenity name cannot be empty."]);
        if (!Enum.TryParse<AmenityType>(amenityData.Type, ignoreCase: true, out var amentiyType))
            return Result<ValidatedData>.Failure(
                ["Invalid Amenity type"]);

        return Result<ValidatedData>.Success(new ValidatedData(amenityData.Name, amentiyType));
    }
}