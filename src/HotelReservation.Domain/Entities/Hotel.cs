
using System.Text.RegularExpressions;

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

    public Hotel() { }

    public record HotelData(
        string Name,
        AddressData Address,
        string Description,
        string PhoneNumber,
        string Email,
        double Rating
    );


    public static Result<Hotel> Create(HotelData data)
    {
        var result = ValidateData(data);
        if (result.IsFailure)
            return Result<Hotel>.Failure(result.Errors);

        var hotel = new Hotel()
        {
            Name = data.Name,
            Address = new Address
            {
                Street = data.Address.Street,
                City = data.Address.City,
                State = data.Address.State,
                ZipCode = data.Address.ZipCode,
                Country = data.Address.Country
            },
            Description = data.Description,
            PhoneNumber = data.PhoneNumber,
            Email = data.Email,
            Rating = data.Rating
        };

        return Result<Hotel>.Success(hotel);
    }

    public static Result<Hotel> Update(Hotel data, HotelData newData)
    {
        var validation = ValidateData(newData);
        if(validation.IsFailure)
            return Result<Hotel>.Failure(validation.Errors);


        data.Name = newData.Name;
        data.Address = new Address
        {
            Street = newData.Address.Street,
            City = newData.Address.City,
            State = newData.Address.State,
            ZipCode = newData.Address.ZipCode,
            Country = newData.Address.Country
        };
        data.Email = newData.Email;
        data.Description = newData.Description;
        data.PhoneNumber = newData.PhoneNumber;
        data.Rating = newData.Rating;

        return Result<Hotel>.Success(data);
    }

    private static Result<HotelData> ValidateData(HotelData data)
    {
        List<string> errors = new();

        if (string.IsNullOrWhiteSpace(data.Name))
            errors.Add("Name is required.");
        if (data.Address is null)
            errors.Add("Address is required.");
        if (string.IsNullOrWhiteSpace(data.Description))
            errors.Add("Description is required.");
        if (!IsValidPhoneNumber(data.PhoneNumber))
            errors.Add("Invalid Egyptian Phone Number");
        if (!IsValidEmail(data.Email))
            errors.Add("Email is invalid");
        if (data.Rating < 0 || data.Rating > 5)
            errors.Add("Rating must be between 0 and 5.");

        if (errors.Count > 0)
            return Result<HotelData>.Failure(errors);

        if (data.Address is not null)
        {
            var addressResult = Address.Create(data.Address);
            if (addressResult.IsFailure)
                return Result<HotelData>.Failure(addressResult.Errors);
        }

        return Result<HotelData>.Success(data);
    }
    
    private static bool IsValidEmail(string email)
    {
        Regex regex = new(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        return regex.IsMatch(email);
    }

    private static bool IsValidPhoneNumber(string phoneNumber)
    {
        Regex regex = new(@"^(\+201|01|00201)[0-2,5]{1}[0-9]{8}$");
        return regex.IsMatch(phoneNumber);
    }
}
