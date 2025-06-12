using HotelReservation.Application.Amenity.Commands.Delete;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Amenity;
public class DeleteEndpoint(IMediator mediator) : BaseController
{
    [HttpDelete("{amenityId:guid}")]
    public async Task<IActionResult> Delete(Guid amenityId)
    {
        var result = await mediator.Send(new Request(amenityId));
        return result.IsSuccess
            ? Ok()
            : HandleFailure(result, "Failed to delete amenity");
    }
}
