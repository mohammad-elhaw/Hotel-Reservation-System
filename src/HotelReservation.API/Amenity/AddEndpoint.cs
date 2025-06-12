using HotelReservation.Application.Amenity.Commands.Add;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Amenity;
public class AddEndpoint(IMediator mediator) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Request request)
    {
        var result = await mediator.Send(request);
        return result.IsSuccess
            ? Ok()
            : HandleFailure(result, "Failed to add amenity");
    }
}
