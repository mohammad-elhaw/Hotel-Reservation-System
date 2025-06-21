using Dapper;
using HotelReservation.Domain;
using HotelReservation.Queries.Hotel;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace HotelReservation.Queries.Reservation.GetById;
public class Repository(IDbConnection connection) : IRepository
{
    public async Task<Result<Domain.Entities.Reservation>> GetById (Guid reservationId)
    {
        var sql = @"
                    SELECT r.Id, r.CheckInDate, r.CheckOutDate, r.CreatedAt, r.TotalPrice, 
                           r.CustomerName, r.CustomerEmail, r.CustomerPhoneNumber, 
                           r.Status, r.HotelId, h.Id, h.Name, 
                           h.Street, h.City, h.State, h.ZipCode, h.Country,
                           h.Description, h.PhoneNumber, h.Email, h.Rating 
                    FROM Reservation r
                    INNER JOIN Hotel h ON r.HotelId = h.Id
                    WHERE r.Id = @reservationId";

        try
        {

            var result = (await connection.QueryAsync<Domain.Entities.Reservation,
                HotelDbModel, Domain.Entities.Reservation>(sql,
                (reservation, hotelData) =>
                {
                    reservation.Hotel = new Domain.Entities.Hotel
                    {
                        Id = hotelData.Id,
                        Name = hotelData.Name,
                        Address = new Domain.Entities.Address
                        {
                            Street = hotelData.Street,
                            City = hotelData.City,
                            State = hotelData.State,
                            ZipCode = hotelData.ZipCode,
                            Country = hotelData.Country
                        },
                        Description = hotelData.Description,
                        PhoneNumber = hotelData.PhoneNumber,
                        Email = hotelData.Email,
                        Rating = hotelData.Rating
                    };
                    return reservation;
                },
                new { reservationId },
                splitOn: "Id")).FirstOrDefault();

            
            return result is null
                ? Result<Domain.Entities.Reservation>.Failure(
                    ["Reservation not found"],
                    StatusCodes.Status404NotFound)
                : Result<Domain.Entities.Reservation>.Success(result);
        }
        catch
        {
            return Result<Domain.Entities.Reservation>.Failure(
                ["Failed to get reservation by id"],
                500);
        }

    }
}
