using Dapper;
using HotelReservation.Domain;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace HotelReservation.Queries.Amenity.GetById;
public class Repository(IDbConnection connection) : IRepository
{
    public async Task<Result<Domain.Entities.Amenity>> GetById(Guid amenityId)
    {
        var sql = @"SELECT * FROM Amenities WHERE Id = @amenityId";

        var result = await connection.QuerySingleOrDefaultAsync<Domain.Entities.Amenity>(sql, 
            new { amenityId });
    
        return result != null 
            ? Result<Domain.Entities.Amenity>.Success(result)
            : Result<Domain.Entities.Amenity>.Failure(
                ["Amenity not found."],
                StatusCodes.Status404NotFound);
    }
}
