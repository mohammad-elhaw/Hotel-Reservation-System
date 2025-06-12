using HotelReservation.Domain;
using MediatR;

namespace HotelReservation.Application.RoomAmenity.Queries.GetAll;
public class Handler(
    HotelReservation.Queries.RoomAmenity.GetAll.IRepository roomAmenityRepo,
    HotelReservation.Queries.Room.Exists.IRepository roomExistsRepo)
    : IRequestHandler<Query, Result<List<Response>>>
{
    public async Task<Result<List<Response>>> Handle(Query request, CancellationToken cancellationToken)
    {
        var roomExistsResult = await roomExistsRepo.Exists(request.RoomId);
        if(roomExistsResult.IsFailure)
            return Result<List<Response>>.Failure(roomExistsResult.Errors, 
                roomExistsResult.StatusCode);

        var roomAmenityresult = await roomAmenityRepo.GetAll(request.RoomId);
        if(roomAmenityresult.IsFailure)
            return Result<List<Response>>.Failure(roomAmenityresult.Errors,
                roomAmenityresult.StatusCode);

        return Result<List<Response>>.Success(roomAmenityresult.Value!
            .Select(ra => new Response
            (Id: ra.Id, Name: ra.Name, Type: ra.Type)).ToList());
    }
}
