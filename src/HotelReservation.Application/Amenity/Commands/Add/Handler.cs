using HotelReservation.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HotelReservation.Application.Amenity.Commands.Add;
public class Handler(
    Infrastructure.Amenity.Add.IRepository commandAddRepo,
    HotelReservation.Queries.Amenity.Exists.IRepository queryExistsRepo) : IRequestHandler<Request, Result>
{
    public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
    {
        var existsResult = await queryExistsRepo.NotExists(request.Name);
        if(existsResult.IsFailure)
            return Result.Failure(existsResult.Errors);

        var amenity = Domain.Entities.Amenity.Create(
            new Domain.Entities.Amenity.AmenityData(request.Name, request.Type));
        if (amenity.IsFailure)
            return Result.Failure(amenity.Errors, amenity.StatusCode);

        commandAddRepo.Add(amenity.Value!);
        int result = await commandAddRepo.SaveChanges();
        return result <= 0
            ? Result.Failure(["Failed to add amenity."],
                StatusCodes.Status500InternalServerError)
            : Result.Success();
    }
}
