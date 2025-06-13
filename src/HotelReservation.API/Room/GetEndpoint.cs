using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Room;
public class GetEndpoint(IMediator mediator): BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll(Guid hotelId)
    {
        var result = await mediator.Send(
            new Application.Room.Queries.GetAll.Query(hotelId));

        return result.IsSuccess
            ? Ok(result.Value)
            : HandleFailure(result, "An error occurred while retrieving the rooms.");
    }

    [HttpGet("{roomId:guid}")]
    public async Task<IActionResult> GetById(Guid hotelId, Guid roomId)
    {
        var result = await mediator.Send(
            new Application.Room.Queries.GetById.Query(hotelId, roomId));

        return result.IsSuccess
            ? Ok(result.Value)
            : HandleFailure(result, "An error occurred while retrieving the room.");
    }
}
