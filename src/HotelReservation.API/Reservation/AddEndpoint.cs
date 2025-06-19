using HotelReservation.Application.Reservation.Commands.Add;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Reservation;
public class AddEndpoint(IMediator mediator) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Request request)
    {
        var result = await mediator.Send(request);

        if(result.IsSuccess && result.StatusCode == StatusCodes.Status409Conflict)
            return Conflict(new
            {
                result.Value,
                result.Code
            });

        return result.IsSuccess
            ? StatusCode(result.StatusCode, result.Value)
            : HandleFailure(result, "Failed To Add Reservation");
    }
}
