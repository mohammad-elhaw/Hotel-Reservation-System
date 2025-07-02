using Dapper;
using HotelReservation.Domain;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace HotelReservation.Queries.Room.CleanUpRooms;
public class Repository(IDbConnection connection) : IRepository
{
    public async Task<Result> CleanupRoomAvailability()
    {
        const string sql = @"
        BEGIN TRANSACTION;
        
        -- Expire reservations (by expiration time)
        UPDATE Reservation
        SET Status = 'Expired'
        WHERE Status = 'Pending' AND GETUTCDATE() >= ExpirationTime;
        
        -- Release rooms that are no longer reserved
        ;WITH RoomsToRelease AS (
            SELECT r.Id
            FROM Room r
            LEFT JOIN ReservationRoom rr ON r.Id = rr.RoomId
            LEFT JOIN Reservation res ON res.Id = rr.ReservationId
            WHERE r.IsAvailable = 0
            GROUP BY r.Id
            HAVING COUNT(CASE 
                WHEN res.Status NOT IN ('Cancelled', 'Expired') 
                    AND res.CheckOutDate >= GETUTCDATE()
                THEN 1 
                ELSE NULL
                END) = 0
        )
        UPDATE r
        SET r.IsAvailable = 1
        FROM Room r
        JOIN RoomsToRelease rtr ON r.Id = rtr.Id;
        
        COMMIT TRANSACTION;";
        // count 1 mean room is excluded 
        // count 0 mean room is included for make it available
        try
        {
            await connection.ExecuteAsync(sql);
            return Result.Success();
        }
        catch
        {
            return Result.Failure(
                ["Cleanup Failed"],
                StatusCodes.Status500InternalServerError);
        }
    }
}
