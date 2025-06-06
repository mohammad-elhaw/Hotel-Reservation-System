using HotelReservation.Application.Room.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Room;
public class GetByIdEndpoint(IMediator mediator) : BaseController
{
    [HttpGet("{roomId:guid}")]
    public async Task<IActionResult> GetById(Guid hotelId, Guid roomId)
    {
        var result = await mediator.Send(new Query(hotelId, roomId));

        if (result.IsFailure)
            return HandleFailure(result, "An error occurred while retrieving the room.");

        return Ok(result.Value);
    }
}
