using Dapper;
using HotelReservation.Domain;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace HotelReservation.Queries.Room.Exists;
public class Repository(IDbConnection connection) : IRepository
{
    public async Task<Result> Exists(Guid roomId)
    {
        var sql = @"
            SELECT COUNT(1) 
            FROM Room
            WHERE Id = @roomId";

        try
        {
            var count = await connection.ExecuteScalarAsync<int>(sql, new {roomId});
            return count > 0
                ? Result.Success()
                : Result.Failure(
                    ["Room does not exist."],
                    StatusCodes.Status404NotFound);
        }
        catch
        {
            return Result.Failure(
                 ["An error occurred while checking room existence"],
                StatusCodes.Status500InternalServerError);
        }
    }
}
