using HotelReservation.Application.Amenity.Commands.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Amenity;
public class UpdateEndpoint(IMediator mediator) : BaseController
{
    [HttpPut("{amenityId}")]
    public async Task<IActionResult> Update(Guid amenityId, [FromBody] Request request)
    {
        var updateRequest = request with
        {
            AmenityId = amenityId
        };
        var result = await mediator.Send(updateRequest);
        return result.IsSuccess
            ? Ok()
            : HandleFailure(result, "Failed to update amenity");
    }
}
