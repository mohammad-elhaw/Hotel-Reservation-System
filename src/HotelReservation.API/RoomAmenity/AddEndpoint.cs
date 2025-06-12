using HotelReservation.Application.RoomAmenity.Commands.Add;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.RoomAmenity;
public class AddEndpoint(IMediator mediator) : BaseController
{
    [HttpPost("{amenityId}")]
    public async Task<IActionResult> AddAmenityToRoom(Guid roomId, Guid amenityId)
    {
        var result = await mediator.Send(new Request(roomId, amenityId));

        return result.IsSuccess
            ? StatusCode(result.StatusCode)
            : HandleFailure(result, "Failed to add room amenity.");
    }


    // we want ability to add collection of amenities to room
}
