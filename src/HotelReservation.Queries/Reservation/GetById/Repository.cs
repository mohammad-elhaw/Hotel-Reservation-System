using Dapper;
using HotelReservation.Domain;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace HotelReservation.Queries.Reservation.GetById;
public class Repository(IDbConnection connection) : IRepository
{
    public async Task<Result<Domain.Entities.Reservation>> GetById(Guid reservationId)
    {
        var sql = @"
                    SELECT * FROM Reservation
                    WHERE Id = @reservationId";

        try
        {
            var result = await connection.QueryFirstOrDefaultAsync<Domain.Entities.Reservation>(sql,
                new { reservationId });

            return result is null
                ? Result<Domain.Entities.Reservation>.Failure(
                    ["Reservation not found"],
                    StatusCodes.Status404NotFound)
                : Result<Domain.Entities.Reservation>.Success(result);
        }
        catch
        {
            return Result<Domain.Entities.Reservation>.Failure(
                ["Failed to get reservation by id"]);
        }

    }
}
