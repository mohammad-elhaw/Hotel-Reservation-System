using Dapper;
using HotelReservation.Domain;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace HotelReservation.Queries.Room.GetAll;
public class Repository(IDbConnection connection) : IRepository
{
    public async Task<Result<List<Domain.Entities.Room>>> GetAll(Guid hotelId)
    {
        try
        {
            var sql = "SELECT * FROM Room WHERE HotelId = @hotelId";
            var rooms = (await connection.QueryAsync<Domain.Entities.Room>(
                sql, new { hotelId })).ToList();

            return Result<List<Domain.Entities.Room>>.Success(rooms);
        }
        catch
        {
            return Result<List<Domain.Entities.Room>>.Failure(new List<string> 
            { "Error Fetching Rooms, Please Try Later." },
            StatusCodes.Status500InternalServerError);
        }
    }
}
