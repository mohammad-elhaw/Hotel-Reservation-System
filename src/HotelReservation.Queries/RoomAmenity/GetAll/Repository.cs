using Dapper;
using HotelReservation.Domain;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace HotelReservation.Queries.RoomAmenity.GetAll;
public class Repository(IDbConnection connection) : IRepository
{
    public async Task<Result<List<Domain.Entities.Amenity>>> GetAll(Guid roomId)
    {
        try
        {
            var sql = @"
                        SELECT a.*
                        FROM Amenities a
                        INNER JOIN RoomAmenity ra ON a.Id = ra.AmenityId
                        WHERE ra.RoomId = @roomId";

            var result = (await connection.QueryAsync<Domain.Entities.Amenity>(
                sql, new { roomId })).ToList();

            if (result.Count == 0)
                return Result<List<Domain.Entities.Amenity>>.Failure(
                    ["No amenities found for the specified room."]);

            return Result<List<Domain.Entities.Amenity>>.Success(result);
        }
        catch
        {
            return Result<List<Domain.Entities.Amenity>>.Failure(
                ["An error occurred while retrieving amenities."],
                StatusCodes.Status500InternalServerError);
        }

    }
}
