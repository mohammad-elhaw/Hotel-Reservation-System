using Dapper;
using HotelReservation.Domain;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace HotelReservation.Queries.Amenity.GetAll;
public class Repository(IDbConnection connection) : IRepository
{
    public async Task<Result<List<Domain.Entities.Amenity>>> GetAll()
    {
        var sql = @"SELECT * FROM Amenities";
        var amenities = (await connection.QueryAsync<Domain.Entities.Amenity>(sql)).ToList();

        return amenities.Count != 0
            ? Result<List<Domain.Entities.Amenity>>.Success(amenities)
            : Result<List<Domain.Entities.Amenity>>.Failure(
                ["No amenities found."],
                StatusCodes.Status404NotFound);

    }
}
