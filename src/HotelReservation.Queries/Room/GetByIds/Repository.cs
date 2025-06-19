using Dapper;
using HotelReservation.Domain;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace HotelReservation.Queries.Room.GetByIds;
public class Repository(IDbConnection connection) : IRepository
{
    public async Task<Result<List<Domain.Entities.Room>>> GetByIds(List<Guid> roomIds, Guid hotelId)
    {
        try
        {
            string sql = @"
                            SELECT r.Id, r.RoomNumber, r.Type, r.IsAvailable,
                            r.Capacity, r.Description, r.HotelId, r.BasePricePerNight, 
                            i.Id AS ImageId, i.ImageUrl  
                            FROM Room r 
                            LEFT JOIN RoomImage i ON r.Id = i.RoomId        
                            WHERE r.Id IN @roomIds AND r.HotelId = @hotelId";

            var roomDic = new Dictionary<Guid, Domain.Entities.Room>();

            await connection.QueryAsync<Domain.Entities.Room,
                Domain.Entities.RoomImage, Domain.Entities.Room>(
                sql, (room, image) =>
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
                }, new {roomIds, hotelId},
                splitOn: "ImageId");

            return Result<List<Domain.Entities.Room>>.Success([.. roomDic.Values]);
        }
        catch
        {
            return Result<List<Domain.Entities.Room>>.Failure(
                ["An error occurred while retrieving the rooms."],
                StatusCodes.Status500InternalServerError);
        }

    }
}
