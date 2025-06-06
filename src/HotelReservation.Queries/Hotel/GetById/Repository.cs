using Dapper;
using HotelReservation.Domain;
using HotelReservation.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace HotelReservation.Queries.Hotel.GetById;

public class Repository(IDbConnection connection) : IRepository
{
    public async Task<Result<Domain.Entities.Hotel>> GetById(Guid id)
    {
        try
        {
            var sql = @"SELECT * FROM Hotel
                        WHERE Id = @Id";

            var hotel = await connection.QueryFirstOrDefaultAsync<HotelDbModel>(sql, new { Id = id });


            if (hotel is not null)
            {
                var hotelEntity = new Domain.Entities.Hotel
                {
                    Id = id,
                    Name = hotel.Name,
                    Address = new Address
                    {
                        Street = hotel.Street,
                        City = hotel.City,
                        ZipCode = hotel.ZipCode,
                        Country = hotel.Country
                    },
                    Description = hotel.Description,
                    Email = hotel.Email,
                    PhoneNumber = hotel.PhoneNumber,
                    Rating = hotel.Rating
                };
                return Result<Domain.Entities.Hotel>.Success(hotelEntity);
            }

            return Result<Domain.Entities.Hotel>.Failure(new List<string>() { "Hotel Not found" });
        }
        catch
        {
            return Result<Domain.Entities.Hotel>.Failure(new List<string>() 
            { "Error Fetching Hotel, Please Try Later." },
            StatusCodes.Status500InternalServerError);
        }
        
    }
}
