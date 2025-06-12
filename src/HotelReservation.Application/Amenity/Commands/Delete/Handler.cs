using HotelReservation.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HotelReservation.Application.Amenity.Commands.Delete;
public class Handler(
    Infrastructure.Amenity.Delete.IRepository amenityRepo,
    HotelReservation.Queries.Amenity.GetById.IRepository getAmenityRepo) 
    : IRequestHandler<Request, Result>
{
    public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
    {
        var amenityResult = await getAmenityRepo.GetById(request.AmenityId);

        if( amenityResult.IsFailure)
            return Result.Failure(amenityResult.Errors, amenityResult.StatusCode);

        amenityRepo.Delete(amenityResult.Value!);
        int result = await amenityRepo.SaveChanges();
        return result > 0
            ? Result.Success()
            : Result.Failure(
                ["Failed to delete Amenity"],
                StatusCodes.Status500InternalServerError);
    }
}
