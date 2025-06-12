using HotelReservation.Application.RoomAmenity.Commands.Delete;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.RoomAmenity;
public class DeleteEndpoint(IMediator mediator) : BaseController
{
    [HttpDelete("{amenityId}")]
    public async Task<IActionResult> Delete(Guid roomId, Guid amenityId)
    {
        var result = await mediator.Send(new Request(roomId, amenityId));
        return result.IsSuccess
            ? NoContent()
            : HandleFailure(result, "Failed To delete the room Amenity");
    }
}
