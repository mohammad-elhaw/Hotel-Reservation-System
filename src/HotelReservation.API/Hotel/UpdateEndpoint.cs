using HotelReservation.Application.Hotel.Command.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Hotel;
public class UpdateEndpoint(IMediator mediator) : BaseController
{
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, 
        [FromBody] Request request)
    {
        var updatedRequest = request with { Id = id };
        var result = await mediator.Send(updatedRequest);

        return result.IsSuccess
            ? Ok(result.Value!)
            : HandleFailure(result, "An error occurred while updating the hotel.");
    }
}
