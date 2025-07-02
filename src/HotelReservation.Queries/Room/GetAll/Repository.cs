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
            var sql = @"
                        SELECT r.*, i.* 
                        FROM Room r 
                        INNER JOIN RoomImage i ON r.Id = i.RoomId 
                        WHERE HotelId = @hotelId";

            var roomDic = new Dictionary<Guid, Domain.Entities.Room>();
            await connection.QueryAsync<Domain.Entities.Room,
                Domain.Entities.RoomImage, Domain.Entities.Room>(
                sql,
                (room, image) =>
                {
                    if (!roomDic.TryGetValue(room.Id, out var existingRoom))
                    {
                        existingRoom = room;
                        existingRoom.Images = new List<Domain.Entities.RoomImage>();
                        roomDic.Add(room.Id, existingRoom);
                    }
                    if (image is not null)
                    {
                        existingRoom.Images.Add(image);
                    }
                    return existingRoom;
                },
                new { hotelId },
                splitOn: "Id"
            );


            if (roomDic.Count == 0)
                return Result<List<Domain.Entities.Room>>.Failure(
                    ["No rooms found for this hotel."],
                    StatusCodes.Status404NotFound);

            var rooms = roomDic.Values.ToList();

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
