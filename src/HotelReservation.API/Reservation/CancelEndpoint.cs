using HotelReservation.Application.Reservation.Commands.Cancel;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Reservation;
public class CancelEndpoint(IMediator mediator) : BaseController
{
    [HttpPut("{reservationId:guid}/cancel")]
    public async Task<IActionResult> Cancel(Guid reservationId)
    {
        var result = await mediator.Send(
            new Request(reservationId));

        return result.IsSuccess
            ? StatusCode(result.StatusCode, new
            {
                result.Code
            })
            : HandleFailure(result, "Failed To Cancel Reservation");
    }
}
