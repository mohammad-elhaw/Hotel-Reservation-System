using HotelReservation.Application.Hotel.Command.Delete;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Hotel;
public class DeleteEndpoint(IMediator mediator) : BaseController
{
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await mediator.Send(new Request(id));

        return result.IsSuccess
            ? NoContent()
            : HandleFailure(result, "Failed to delete hotel.");
    }
}
