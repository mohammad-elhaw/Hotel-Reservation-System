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
            var sql = @"
                        SELECT r.*, i.* 
                        FROM Room r 
                        LEFT JOIN RoomImage i ON r.Id = i.RoomId        
                        WHERE r.Id = @roomId AND HotelId = @hotelId";

            var roomDic = new Dictionary<Guid, Domain.Entities.Room>();
            
            await connection.QueryAsync<Domain.Entities.Room, 
                Domain.Entities.RoomImage, Domain.Entities.Room>(
                sql,
                (room, image) =>
                {
                    if (!roomDic.TryGetValue(room.Id, out var existingRoom))
                    {
                        existingRoom = room;
                        existingRoom.Images = [];
                        roomDic.Add(room.Id, existingRoom);
                    }
                    if (image != null && image.Id != Guid.Empty)
                    {
                        existingRoom.Images.Add(image);
                    }
                    return existingRoom;
                },
                new { roomId, hotelId },
                splitOn: "Id"
            );


            var roomResult = roomDic.Values.FirstOrDefault();

            if(roomResult is null)
                return Result<Domain.Entities.Room>.Failure(
                    ["Room not found."],
                    StatusCodes.Status404NotFound);

            return Result<Domain.Entities.Room>.Success(roomResult);
        }
        catch
        {
            return Result<Domain.Entities.Room>.Failure(
                ["An error occurred while retrieving the room."],
                StatusCodes.Status500InternalServerError);
        }

    }
}
