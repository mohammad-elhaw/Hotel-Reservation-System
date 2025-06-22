using HotelReservation.Domain;
using MediatR;

namespace HotelReservation.Application.Room.Commands.Update;
public class Handler (
    HotelReservation.Queries.Hotel.Exists.IRepository hotelExistsRepo,
    HotelReservation.Queries.Room.GetById.IRepository getRoomRepo,
    Infrastructure.Room.Update.IRepository updateRoomRepo)
    : IRequestHandler<Request, Result<Response>>
{
    public async Task<Result<Response>> Handle(Request request, CancellationToken cancellationToken)
    {
        var existsResult = await hotelExistsRepo.Exists(request.HotelId);

        if (existsResult.IsFailure)
            return Result<Response>.Failure(existsResult.Errors, existsResult.StatusCode);

        if (existsResult.IsSuccess && !existsResult.Value)
            return Result<Response>.Failure(["Hotel Not Found"], 404);


        var roomResult = await getRoomRepo.GetById(request.HotelId, request.RoomId);

        if(roomResult.IsFailure)
            return Result<Response>.Failure(roomResult.Errors, roomResult.StatusCode);

        var roomDataToUpdate = new Domain.Entities.Room.UpdateRoomData(
            request.RoomNumber,
            request.Type,
            request.IsAvailable,
            request.Capacity,
            request.Description,
            request.HotelId);

        var updatedResult = Domain.Entities.Room.Update(roomResult.Value!,
            roomDataToUpdate);

        if(updatedResult.IsFailure)
            return Result<Response>.Failure(updatedResult.Errors, updatedResult.StatusCode);

        updateRoomRepo.Update(updatedResult.Value!);
        int saveResult = await updateRoomRepo.SaveChanges();

        return saveResult > 0
            ? Result<Response>.Success(new Response(updatedResult.Value!.Id))
            : Result<Response>.Failure(["Failed to update room"], 500);
    }
}
