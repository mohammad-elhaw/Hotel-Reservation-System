using HotelReservation.Domain;
using MediatR;

namespace HotelReservation.Application.RoomAmenity.Queries.GetById;
public class Handler(
    HotelReservation.Queries.Room.Exists.IRepository roomExistsRepo,
    HotelReservation.Queries.Amenity.Exists.IRepository amenityExistsRepo,
    HotelReservation.Queries.RoomAmenity.GetById.IRepository amenityRepo) 
    : IRequestHandler<Query, Result<Response>>
{
    public async Task<Result<Response>> Handle(Query request, CancellationToken cancellationToken)
    {
        var roomExistsResult = await roomExistsRepo.Exists(request.RoomId);

        if (roomExistsResult.IsFailure)
            return Result<Response>.Failure(roomExistsResult.Errors,
                roomExistsResult.StatusCode);

        var amenityExistsResult = await amenityExistsRepo.Exists(request.AmenityId);
        if (amenityExistsResult.IsFailure)
            return Result<Response>.Failure(amenityExistsResult.Errors,
                amenityExistsResult.StatusCode);


        var amenityResult = await amenityRepo.GetById(request.RoomId, request.AmenityId);
        if (amenityResult.IsFailure)
            return Result<Response>.Failure(amenityResult.Errors,
                amenityResult.StatusCode);

        return Result<Response>.Success(new Response(
            Id: amenityResult.Value!.Id,
            Name: amenityResult.Value.Name,
            Type: amenityResult.Value.Type));
    }
}
