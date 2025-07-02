using Dapper;
using HotelReservation.Domain;
using HotelReservation.Domain.Entities.Enums;
using HotelReservation.Queries.Hotel;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace HotelReservation.Queries.Reservation.GetAll;
public class Repository(IDbConnection connection) : IRepository
{
    public async Task<Result<List<Domain.Entities.Reservation>>> GetAllReservations(Guid hotelId)
    {
        string sql = @"
                    SELECT * 
                    FROM Reservation r
                    INNER JOIN Hotel h ON r.HotelId = h.Id
                    WHERE h.Id = @hotelId";
        try
        {
            var result = (await connection.QueryAsync<Domain.Entities.Reservation,
                HotelDbModel, Domain.Entities.Reservation>(
                sql,(reservation, hotelData) =>
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
                new { hotelId }, 
                splitOn: "Id")).ToList();

            return result.Count <= 0
                ? Result<List<Domain.Entities.Reservation>>.Success(
                    [], StatusCodes.Status404NotFound,
                    "Not found.")
                : Result<List<Domain.Entities.Reservation>>.Success(result);

        }
        catch
        {
            return Result<List<Domain.Entities.Reservation>>.Failure(
                ["Failed to retrieve reservations from the database."],
                StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<Dictionary<Guid, List<(DateTime CheckIn, DateTime CheckOut, 
        BookingStatus Status)>>> GetValidReservationsForRooms(List<Guid> roomIds)
    {
        string sql = @"
                    SELECT rr.RoomId, r.CheckInDate, r.CheckOutDate, r.Status
                    FROM Reservation r
                    INNER JOIN ReservationRoom rr ON rr.ReservationId = r.Id
                    WHERE rr.RoomId IN @roomIds AND r.Status = 'Confirmed'";


        var result = await connection.QueryAsync<(Guid RoomId, DateTime CheckInDate,
            DateTime CheckOutDate, BookingStatus Status)>(
            sql, new { roomIds });

        return result.GroupBy(r => r.RoomId)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => (x.CheckInDate, x.CheckOutDate, x.Status)).ToList());
    }
}
