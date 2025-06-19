using HotelReservation.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HotelReservation.Application.Room.Commands.Delete;
public class Handler(
    HotelReservation.Queries.Room.GetById.IRepository roomGetRepo,
    Infrastructure.Room.Delete.IRepository roomDeleteRepo) 
    : IRequestHandler<Request, Result>
{
    public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
    {
        var roomResult = await roomGetRepo.GetById(request.HotelId, request.RoomId);
        if (roomResult.IsFailure)
            return Result.Failure(roomResult.Errors, roomResult.StatusCode);

        roomDeleteRepo.Delete(roomResult.Value!);
        int deleteResult = await roomDeleteRepo.SaveChanges();

        return deleteResult > 0
            ? Result.Success(StatusCodes.Status204NoContent)
            : Result.Failure(["Failed to delete room."], 
                StatusCodes.Status500InternalServerError);
    }
}
