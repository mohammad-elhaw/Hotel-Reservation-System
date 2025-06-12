using Dapper;
using HotelReservation.Domain;
using System.Data;

namespace HotelReservation.Queries.RoomImage.GetById;
public class Repository(IDbConnection connection) : IRepository
{
    public async Task<Result<Domain.Entities.RoomImage>> GetById(Guid imageId)
    {
        try
        {
            var query = @"
                                SELECT *
                                FROM RoomImage ri
                                WHERE ri.Id = @imageId";
            
            var result = await connection
                .QueryFirstOrDefaultAsync<Domain.Entities.RoomImage>(query, new {imageId});

            if (result is null)
                return Result<Domain.Entities.RoomImage>.Failure(new List<string>
            { "Image not found." });

            return Result<Domain.Entities.RoomImage>.Success(result);
        }
        catch
        {
            return Result<Domain.Entities.RoomImage>.Failure(new List<string>
            { "An error occurred while retrieving the image." });
        }
        
    }
}
