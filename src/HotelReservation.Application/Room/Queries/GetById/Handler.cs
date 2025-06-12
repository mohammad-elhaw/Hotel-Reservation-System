using HotelReservation.Domain;
using MediatR;

namespace HotelReservation.Application.Room.Queries.GetById;
public class Handler(
    HotelReservation.Queries.Hotel.GetById.IRepository hotelRepo,
    HotelReservation.Queries.Room.GetById.IRepository roomRepo) 
    : IRequestHandler<Query, Result<Response>>
{
    public async Task<Result<Response>> Handle(Query request, CancellationToken cancellationToken)
    {
        var hotelResult = await hotelRepo.GetById(request.HotelId);
        if (hotelResult.IsFailure)
            return Result<Response>.Failure(hotelResult.Errors);

        var roomResult = await roomRepo.GetById(request.HotelId, request.RoomId);
        if(roomResult.IsFailure)
            return Result<Response>.Failure(roomResult.Errors);

        return Result<Response>.Success(new Response(
            Id: roomResult.Value!.Id,
            RoomNumber: roomResult.Value!.RoomNumber,
            Type: roomResult.Value!.Type,
            IsAvailable: roomResult.Value!.IsAvailable,
            Capacity: roomResult.Value!.Capacity,
            Description: roomResult.Value!.Description,
            HotelId: roomResult.Value!.HotelId,
            Images: roomResult.Value!.Images
                .Select(ri => new RoomImageResponse(ri.Id,  ri.ImageUrl ))
                .ToList()));
    }
}
