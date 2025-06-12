using Dapper;
using HotelReservation.Domain;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace HotelReservation.Queries.RoomAmenity.GetById;
public class Repository(IDbConnection connection) : IRepository
{
    public async Task<Result<Domain.Entities.Amenity>> GetById(Guid roomId, Guid amenityId)
    {
        var sql = @"
                        SELECT a.*
                        FROM Amenities a
                        INNER JOIN RoomAmenity ra ON a.Id = ra.AmenityId
                        WHERE ra.RoomId = @roomId AND a.Id = @amenityId";
        try
        {
            var result = await connection.QueryFirstOrDefaultAsync<Domain.Entities.Amenity>(sql,
                new { roomId, amenityId });

            if (result is null)
                return Result<Domain.Entities.Amenity>.Failure(
                    [$"This room don't have amenity with id: {amenityId}"],
                    StatusCodes.Status404NotFound);

            return Result<Domain.Entities.Amenity>.Success(result);

        }
        catch
        {
            return Result<Domain.Entities.Amenity>.Failure(
                ["An error occurred while retrieving data."],
                StatusCodes.Status500InternalServerError);
        }

    }
}
