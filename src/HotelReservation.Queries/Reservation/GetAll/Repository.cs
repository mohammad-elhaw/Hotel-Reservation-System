using Dapper;
using HotelReservation.Domain.Entities.Enums;
using System.Data;

namespace HotelReservation.Queries.Reservation.GetAll;
public class Repository(IDbConnection connection) : IRepository
{
    public async Task<Dictionary<Guid, List<(DateTime CheckIn, DateTime CheckOut, 
        BookingStatus Status)>>> GetReservationsForRooms(List<Guid> roomIds)
    {
        var sql = @"
                    SELECT rr.RoomId, r.CheckInDate, r.CheckOutDate, r.Status
                    FROM Reservation r
                    INNER JOIN ReservationRoom rr ON rr.ReservationId = r.Id
                    WHERE rr.RoomId IN @roomIds";


        var result = await connection.QueryAsync<(Guid RoomId, DateTime CheckInDate,
            DateTime CheckOutDate, BookingStatus Status)>(
            sql, new { roomIds });

        return result.GroupBy(r => r.RoomId)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => (x.CheckInDate, x.CheckOutDate, x.Status)).ToList());
    }
}
