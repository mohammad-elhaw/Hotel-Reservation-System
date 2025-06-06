using HotelReservation.Application.Hotel.Command.Add;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Hotel.Add;
public class Endpoint(IMediator mediator) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> AddHotel([FromBody] Request request)
    {
        var result = await mediator.Send(request);

        if (result.IsFailure)
            return HandleFailure(result, "Failed to create hotel.");

        return Ok(result.Value);
    }
}
