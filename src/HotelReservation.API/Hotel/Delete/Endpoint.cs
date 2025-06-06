using HotelReservation.Application.Hotel.Command.Delete;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Hotel.Delete;
public class Endpoint(IMediator mediator) : BaseController
{
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await mediator.Send(new Request(id));
        if (result.IsFailure)
            return HandleFailure(result, "Failed to delete hotel.");

        return NoContent();
    }
}
