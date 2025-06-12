using Dapper;
using HotelReservation.Domain;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace HotelReservation.Queries.Amenity.Exists;
public class Repository(IDbConnection connection) : IRepository
{
    public async Task<Result> NotExists(string name)
    {
        var sql = @"
                    SELECT COUNT(1)
                    FROM Amenities 
                    WHERE Name = @name";

        int count = await connection.ExecuteScalarAsync<int>(sql, new { name });

        return count > 0
            ? Result.Failure([$"Amenity {name} is already exists"])
            : Result.Success();
    }

    public async Task<Result> NotExists(string name, Guid amenityId)
    {
        var sql = @"
                    SELECT COUNT(1)
                    FROM Amenities 
                    WHERE Name = @name AND Id != @id";

        int count = await connection.ExecuteScalarAsync<int>(sql, new { name , amenityId });

        return count > 0
            ? Result.Failure([$"Amenity {name} is already exists"])
            : Result.Success();
    }

    public async Task<Result> Exists(Guid amenityId)
    {
        var sql = @"
                    SELECT COUNT(1)
                    FROM Amenities 
                    WHERE Id = @amenityId";

        int count = await connection.ExecuteScalarAsync<int>(sql, new { amenityId });

        return count > 0
            ? Result.Success()
            : Result.Failure(
                [$"Amenity with id : {amenityId} is not exist"],
                StatusCodes.Status404NotFound);
    }
}
