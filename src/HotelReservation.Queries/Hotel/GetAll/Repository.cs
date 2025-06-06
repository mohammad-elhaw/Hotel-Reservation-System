using Dapper;
using HotelReservation.Domain;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace HotelReservation.Queries.Hotel.GetAll;
public class Repository(IDbConnection connection) : IRepository
{
    public async Task<Result<List<Domain.Entities.Hotel>>> GetAll()
    {
        try
        {
            var sql = "SELECT * FROM Hotel";
            var hotelsModel = (await connection.QueryAsync<HotelDbModel>(sql)).ToList();

            var hotelsEntity = hotelsModel.Select(h => new Domain.Entities.Hotel
            {
                Id = h.Id,
                Name = h.Name,
                Address = new Domain.Entities.Address
                {
                    Street = h.Street,
                    City = h.City,
                    State = h.State,
                    ZipCode = h.ZipCode,
                    Country = h.Country
                },
                Description = h.Description,
                PhoneNumber = h.PhoneNumber,
                Email = h.Email,
                Rating = h.Rating
            }).ToList();
            return Result<List<Domain.Entities.Hotel>>.Success(hotelsEntity);
        }
        catch
        {
            return Result<List<Domain.Entities.Hotel>>.Failure(new List<string> 
            { "Error fetching hotels, Please Try Later" },
            StatusCodes.Status500InternalServerError);
        }
    }
}
