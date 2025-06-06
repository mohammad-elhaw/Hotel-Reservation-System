using HotelReservation.Application.Hotel.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Hotel.Get;
public class Endpoint(IMediator mediator) : BaseController
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await mediator.Send(new Query(id));
        if (result.IsFailure)
            return HandleFailure(result, "An error occurred while retrieving the hotel.");

        return Ok(result.Value);
    }

}
