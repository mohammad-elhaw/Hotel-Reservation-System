using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.RoomAmenity;
public class GetEndpoint(IMediator mediator) : BaseController
{
    [HttpGet]
    public async Task<IActionResult>GetAll(Guid roomId)
    {
        var result = await mediator.Send(
            new Application.RoomAmenity.Queries.GetAll.Query(roomId));
        return result.IsSuccess
            ? Ok(result.Value)
            : HandleFailure(result, "Failed to get room amenities.");
    }

    [HttpGet("{amenityId}")]
    public async Task<IActionResult> GetById(Guid roomId, Guid amenityId)
    {
        var result = await mediator.Send(
            new Application.RoomAmenity.Queries.GetById.Query(roomId, amenityId));
        return result.IsSuccess
            ? Ok(result.Value)
            : HandleFailure(result, "Failed to get room amenity by id.");
    }
}
