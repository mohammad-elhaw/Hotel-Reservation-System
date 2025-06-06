namespace HotelReservation.Domain.Entities;

public record AddressData(
    string Street,
    string City,
    string State,
    string ZipCode,
    string Country
);
public class Address
{
    public string Street { get; set; } = null!;
    public string City { get; set; } = null!;
    public string State { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    public string Country { get; set; } = null!;
    

    public static Result<Address> Create(AddressData data)
    {
        var result = ValidateData(data);

        if (result.IsFailure)
            return Result<Address>.Failure(result.Errors);

        return Result<Address>.Success(
            new Address
            {
                Street = data.Street,
                City = data.City,
                State = data.State,
                ZipCode = data.ZipCode,
                Country = data.Country
            });
    }

    private static Result<AddressData> ValidateData(AddressData address)
    {
        List<string> errors = new();

        if (string.IsNullOrWhiteSpace(address.Street))
            errors.Add("Street is required.");
        if (string.IsNullOrWhiteSpace(address.City))
            errors.Add("City is required.");
        if (string.IsNullOrWhiteSpace(address.State))
            errors.Add("State is required.");
        if (string.IsNullOrWhiteSpace(address.ZipCode))
            errors.Add("Zip code is required.");
        if (string.IsNullOrWhiteSpace(address.Country))
            errors.Add("Country is required.");

        if (errors.Count > 0)
            return Result<AddressData>.Failure(errors);
        
        return Result<AddressData>.Success(address);
    }
}