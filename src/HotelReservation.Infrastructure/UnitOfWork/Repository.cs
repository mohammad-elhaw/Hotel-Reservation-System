
using HotelReservation.Domain;
using Microsoft.AspNetCore.Http;

namespace HotelReservation.Infrastructure.UnitOfWork;
public class Repository(HotelReservationDbContext context) : IRepository
{
    public async Task<Result> SaveChanges()
    {
        try
        {
            int result = await context.SaveChangesAsync();
            if(result == 0)
                return Result.Success(
                    code: "No changes",
                    statusCode: StatusCodes.Status409Conflict);

            return Result.Success(code: "Saved Successfully");
        }
        catch
        {
            return Result.Failure(
                ["Failed To Save Changes"],
                StatusCodes.Status500InternalServerError);
        }
    }
}
