using Dapper;
using HotelReservation.Domain;
using System.Data;

namespace HotelReservation.Queries.RoomAmenity.NotExists;
public class Repository(IDbConnection connection) : IRepository
{
    public async Task<Result> NotExists(Guid roomId, Guid amenityId)
    {

        var sql = @"
                    SELECT COUNT(1)
                    FROM RoomAmenity
                    WHERE RoomId = @roomId AND AmenityId = @amenityId";

        int count = await connection.ExecuteScalarAsync<int>(sql, new { roomId, amenityId });

        return count <= 0 
            ? Result.Success()
            : Result.Failure(
                [$"Amenity with Id {amenityId} already exists for room ID {roomId}."]);

    }
}
