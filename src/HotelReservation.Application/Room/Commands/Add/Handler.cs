using HotelReservation.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HotelReservation.Application.Room.Commands.Add;
public class Handler(
    Infrastructure.Room.Add.IRepository roomRepo,
    HotelReservation.Queries.Hotel.GetById.IRepository hotelRepo) : IRequestHandler<Request, Result>
{
    public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
    {
        var hotelResult = await hotelRepo.GetById(request.HotelId);

        if (hotelResult.IsFailure)
            return Result.Failure(hotelResult.Errors);

        var result = Domain.Entities.Room.Create
        (
            new Domain.Entities.Room.CreateRoomData
            (
                request.RoomNumber,
                request.Type,
                request.Capacity,
                request.Description,
                request.HotelId
            )
        );
        if (result.IsFailure)
            return Result.Failure(result.Errors);

        roomRepo.Add(result.Value!);
        var addingResult = await roomRepo.SaveChanges();

        if(addingResult <= 0)
            return Result.Failure(new List<string>{"Failed to add room"}, 
                statusCode:StatusCodes.Status500InternalServerError);

        return Result.Success();
    }
}
