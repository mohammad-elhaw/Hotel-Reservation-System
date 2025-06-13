using HotelReservation.Application.RoomImage.Command.Add;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Room.Image;
public class AddEndpoint(IMediator mediator) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> AddImage(Guid hotelId, Guid roomId, 
        [FromForm] List<IFormFile> images)
    {
        if(images == null || images.Count == 0)
            return BadRequest("At least one image is required.");

        var result = await mediator.Send(new Request(hotelId, roomId, images));
        
        return result.IsSuccess
            ? Ok()
            : StatusCode(result.StatusCode, result.Errors);
    }
}
