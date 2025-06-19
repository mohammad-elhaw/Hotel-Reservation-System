using HotelReservation.Application.Room.Commands.Delete;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Room;
public class DeleteEndpoint(IMediator mediator) : BaseController
{
    [HttpDelete("{roomId}")]
    public async Task<IActionResult> Delete(Guid hotelId, Guid roomId)
    {
        var result = await mediator.Send(new Request(hotelId, roomId));

        return result.IsSuccess
            ? StatusCode(result.StatusCode)
            : HandleFailure(result, "Failed To delete room");
    }
}
