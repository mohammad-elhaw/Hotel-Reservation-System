using HotelReservation.Application.RoomImage.Command.Delete;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Room.Image.Endpoint;
public class Delete(IMediator mediator) : BaseController
{
    [HttpDelete("{imageId:guid}")]
    public async Task<IActionResult> DeleteImage( Guid imageId)
    {
        if (string.IsNullOrEmpty(imageId.ToString()))
            return BadRequest("Image URL is required.");

        var result = await mediator.Send(new Request(imageId));
        return result.IsSuccess
            ? Ok()
            : HandleFailure(result, "Failure for deleting image");
    }
}
