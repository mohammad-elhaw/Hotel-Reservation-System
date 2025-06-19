using HotelReservation.Application.Reservation.Commands.Delete;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Reservation;
public class DeleteEndpoint(IMediator mediator) : BaseController
{
    [HttpDelete("{reservationId:guid}")]
    public async Task<IActionResult> Delete( Guid reservationId)
    {
        var result = await mediator.Send(new Request(reservationId));
        return result.IsSuccess
            ? StatusCode(result.StatusCode)
            : HandleFailure(result, "Failed to delete reservation");
    }
}
