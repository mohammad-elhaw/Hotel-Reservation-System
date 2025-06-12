using HotelReservation.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HotelReservation.Application.RoomAmenity.Commands.Add;
public class Handler(
    HotelReservation.Queries.Amenity.Exists.IRepository amenityRepo,
    HotelReservation.Queries.Room.Exists.IRepository roomRepo,
    HotelReservation.Queries.RoomAmenity.NotExists.IRepository roomAmenityexistsRepo,
    Infrastructure.RoomAmenity.Add.IRepository roomAmenityAddRepo) 
    : IRequestHandler<Request, Result>
{
    public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
    {
        var isAmenityExistsResult = await amenityRepo.Exists(request.AmenityId);
        if (isAmenityExistsResult.IsFailure)
            return Result.Failure(isAmenityExistsResult.Errors,
                isAmenityExistsResult.StatusCode);

        var isRoomExistsResult = await roomRepo.Exists(request.RoomId);
        if (isRoomExistsResult.IsFailure)
            return Result.Failure(isRoomExistsResult.Errors,
                isRoomExistsResult.StatusCode);

        var isAmenityNotExistsForRoomResult = await roomAmenityexistsRepo
            .NotExists(request.RoomId, request.AmenityId);

        if (isAmenityNotExistsForRoomResult.IsFailure)
            return Result.Failure(isAmenityNotExistsForRoomResult.Errors);

        roomAmenityAddRepo.Add(new Domain.Entities.RoomAmenity
        {
            RoomId = request.RoomId,
            AmenityId = request.AmenityId
        });

        var saveResult = await roomAmenityAddRepo.SaveChanges();

        if (saveResult <= 0)
            return Result.Failure(["Failed To Add room Amenity"], 
                StatusCodes.Status500InternalServerError);

        return Result.Success(StatusCodes.Status201Created);
    }
}
