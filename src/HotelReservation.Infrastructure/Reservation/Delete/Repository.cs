using HotelReservation.Domain;
using Microsoft.AspNetCore.Http;

namespace HotelReservation.Infrastructure.Reservation.Delete;
public class Repository(HotelReservationDbContext context) : IRepository
{
    public void Delete(Domain.Entities.Reservation reservation) =>
        context.Set<Domain.Entities.Reservation>().Remove(reservation);

    public async Task<Result> SaveChanges()
    {
        try
        {

            int result = await context.SaveChangesAsync();
            return result > 0
                ? Result.Success( StatusCodes.Status204NoContent)
                : Result.Failure(["Failed to delete reservation"],
                    StatusCodes.Status500InternalServerError);
        }
        catch
        {
            return Result.Failure(
                ["An error occurred while deleting the reservation."],
                StatusCodes.Status500InternalServerError);
        }
    }
}
