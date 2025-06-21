using Dapper;
using HotelReservation.Domain;
using System.Data;

namespace HotelReservation.Queries.Hotel.Exists;
public class Repository(IDbConnection connection) : IRepository
{
    public async Task<Result<bool>> Exists(Guid hotelId)
    {
        string sql = @"
                SELECT COUNT(1) 
                FROM Hotel 
                WHERE Id = @hotelId";

        try
        {
            int result = await connection.ExecuteScalarAsync<int>(sql, new { hotelId });

            return result > 0
                ? Result<bool>.Success(true)
                : Result<bool>.Success(false, 404);
        }
        catch
        {
            return Result<bool>.Failure(
                ["Error when fetching data from database."],
                500);
        }
    }
}
