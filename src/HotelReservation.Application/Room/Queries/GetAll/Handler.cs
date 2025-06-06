using HotelReservation.Domain;
using MediatR;

namespace HotelReservation.Application.Room.Queries.GetAll;
public class Handler(
    HotelReservation.Queries.Hotel.GetById.IRepository hotelRepo,
    HotelReservation.Queries.Room.GetAll.IRepository roomRepo) 
    : IRequestHandler<Query, Result<List<Response>>>
{
    public async Task<Result<List<Response>>> Handle(Query request, CancellationToken cancellationToken)
    {
        var hotelResult = await hotelRepo.GetById(request.HotelId);
        if(hotelResult.IsFailure)
            return Result<List<Response>>.Failure(hotelResult.Errors);

        var roomsResult = await roomRepo.GetAll(request.HotelId);
        if (roomsResult.IsFailure)
            return Result<List<Response>>.Failure(roomsResult.Errors);

        var response = roomsResult.Value!
            .Select(room => new Response(
                room.Id,
                room.RoomNumber,
                room.Type,
                room.IsAvailable,
                room.Capacity,
                room.Description,
                room.HotelId))
            .ToList();

        return Result<List<Response>>.Success(response);
    }
}
