using Dapper;
using HotelReservation.Domain;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace HotelReservation.Queries.RoomAmenity.Get;
public class Repository(IDbConnection connection) : IRepository
{
    public async Task<Result<Domain.Entities.RoomAmenity>> GetRoomAmenity(Guid roomId, Guid amenityId)
    {
        var sql = @"
                        SELECT RoomId, AmenityId
                        FROM RoomAmenity
                        WHERE RoomId = @roomId AND AmenityId = @amenityId";

        var result = await connection.QueryFirstOrDefaultAsync<
            Domain.Entities.RoomAmenity>(sql, new { roomId, amenityId });

        return result is null
            ? Result<Domain.Entities.RoomAmenity>.Failure(
                [$"No RoomAmenity found for RoomId '{roomId}' and AmenityId '{amenityId}'."],
                StatusCodes.Status404NotFound)
            : Result<Domain.Entities.RoomAmenity>.Success(result);

    }
}
