using HotelReservation.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using static HotelReservation.Domain.Entities.Amenity;

namespace HotelReservation.Application.Amenity.Commands.Update;
public class Handler(
    HotelReservation.Queries.Amenity.GetById.IRepository getAmenityRepo,
    Infrastructure.Amenity.Update.IRepository amenityRepo,
    HotelReservation.Queries.Amenity.Exists.IRepository amenityExistsRepo) 
    : IRequestHandler<Request, Result>
{
    public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
    {
        var amenityResult = await getAmenityRepo.GetById(request.AmenityId);
        
        if(amenityResult.IsFailure)
            return Result.Failure(amenityResult.Errors, amenityResult.StatusCode);

        var existsResult = await amenityExistsRepo.NotExists(request.Name, request.AmenityId);
        if (existsResult.IsFailure)
            return Result.Failure(existsResult.Errors, existsResult.StatusCode);

        var result = Domain.Entities.Amenity.Update(amenityResult.Value!,
            new AmenityData(request.Name, request.Type));

        if (result.IsFailure)
            return Result.Failure(result.Errors, result.StatusCode);
        
        amenityRepo.Update(amenityResult.Value!);
        int saveResult = await amenityRepo.SaveChanges();

        return saveResult > 0
            ? Result.Success()
            : Result.Failure(
                ["Failed to update Amenity"],
                StatusCodes.Status500InternalServerError);
    }
}
