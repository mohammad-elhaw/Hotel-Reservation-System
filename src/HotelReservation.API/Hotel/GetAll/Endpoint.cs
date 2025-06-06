using HotelReservation.Application.Hotel.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Hotel.GetAll;
public class Endpoint(IMediator mediator) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new Query());
        if (result.IsFailure)
            return HandleFailure(result, "An error occurred while retrieving the hotels.");

        return Ok(result.Value);
    }
}
