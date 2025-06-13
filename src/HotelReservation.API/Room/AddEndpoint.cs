using HotelReservation.Application.Room.Commands.Add;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Room;
public class AddEndpoint(IMediator mediator) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> AddRoom(Guid hotelId, [FromBody] Request request)
    {
        var updatedRequest = request with { HotelId = hotelId };
        var result = await mediator.Send(updatedRequest);
        
        return result.IsSuccess
            ? StatusCode(StatusCodes.Status201Created)
            : HandleFailure(result, "Failed to add room");
    }
}
