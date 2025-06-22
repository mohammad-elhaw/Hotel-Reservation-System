using Microsoft.AspNetCore.Mvc;

using HotelReservation.Application.Room.Commands.Update;
using MediatR;

namespace HotelReservation.API.Room;
public class UpdateEndpoint(IMediator mediator) : BaseController
{
    [HttpPut("{roomId:guid}")]
    public async Task<IActionResult> Update(
        Guid hotelId, Guid roomId,
        [FromBody] Request request)
    {
        var updatedRequest = request with
        {
            HotelId = hotelId,
            RoomId = roomId
        };
        var result = await mediator.Send(updatedRequest);
        return result.IsFailure
            ? HandleFailure(result, "Failed to update room")
            : Ok(result.Value);
    }
}
