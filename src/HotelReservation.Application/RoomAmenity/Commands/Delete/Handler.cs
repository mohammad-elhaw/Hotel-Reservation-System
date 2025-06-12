using HotelReservation.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HotelReservation.Application.RoomAmenity.Commands.Delete;
public class Handler(
    Infrastructure.RoomAmenity.Delete.IRepository deleteRoomAmenityRepo,
    HotelReservation.Queries.RoomAmenity.Get.IRepository getRoomAmenityRepo) 
    : IRequestHandler<Request, Result>
{
    public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
    {
        var roomAmenityResult = await getRoomAmenityRepo
            .GetRoomAmenity(request.RoomId, request.AmenityId);

        if(roomAmenityResult.IsFailure)
            return Result.Failure(roomAmenityResult.Errors, roomAmenityResult.StatusCode);
        
        deleteRoomAmenityRepo.Delete(roomAmenityResult.Value!);
        int saveResult = await deleteRoomAmenityRepo.SaveChanges();
        return saveResult > 0
            ? Result.Success()
            : Result.Failure(["Failed to delete room amenity"], 
            StatusCodes.Status500InternalServerError);
    }
}
