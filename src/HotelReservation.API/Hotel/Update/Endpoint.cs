using HotelReservation.Application.Hotel.Command.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Hotel.Update;
public class Endpoint(IMediator mediator) : BaseController
{
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, 
        [FromBody] Request request)
    {
        var updatedRequest = request with { Id = id };
        var result = await mediator.Send(updatedRequest);

        if (result.IsFailure)
            return HandleFailure(result, "An error occurred while updating the hotel.");

        return Ok(result.Value!);
    }
}
