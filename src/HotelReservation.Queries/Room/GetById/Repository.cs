using Dapper;
using HotelReservation.Domain;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace HotelReservation.Queries.Room.GetById;
public class Repository(IDbConnection connection) : IRepository
{
    public async Task<Result<Domain.Entities.Room>> GetById(Guid hotelId, Guid roomId)
    {
        try
        {
            var sql = "SELECT * FROM Room WHERE Id = @roomId AND HotelId = @hotelId";
            var room = await connection.QueryFirstOrDefaultAsync<Domain.Entities.Room>(
                sql, new { roomId, hotelId });
            if(room is null)
                return Result<Domain.Entities.Room>.Failure(
                    ["Room not found."]);
            return Result<Domain.Entities.Room>.Success(room);
        }
        catch
        {
            return Result<Domain.Entities.Room>.Failure(
                ["An error occurred while retrieving the room."],
                StatusCodes.Status500InternalServerError);
        }

    }
}
