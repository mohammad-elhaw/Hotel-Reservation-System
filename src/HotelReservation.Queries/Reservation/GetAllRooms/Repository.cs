using Dapper;
using HotelReservation.Domain;
using System.Data;

namespace HotelReservation.Queries.Reservation.GetAllRooms;
public class Repository(IDbConnection connection) : IRepository
{
    public async Task<Result<List<Domain.Entities.Room>>> GetAllRooms(Guid reservationId)
    {
        string sql = @"
                SELECT r.Id, r.RoomNumber, r.Type, r.IsAvailable,
                r.Capacity, r.Description, r.HotelId, r.BasePricePerNight
                FROM Room r
                INNER JOIN ReservationRoom rr ON r.Id = rr.RoomId
                WHERE rr.ReservationId = @reservationId";
        try
        {
            var rooms = (await connection.QueryAsync<Domain.Entities.Room>(sql,
                new { reservationId })).ToList();

            return Result<List<Domain.Entities.Room>>.Success(rooms);
        }
        catch
        {
            return Result<List<Domain.Entities.Room>>.Failure(
                ["An error occurred while fetching rooms."], 500);
        }

    }
}
