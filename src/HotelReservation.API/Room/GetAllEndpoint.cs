using HotelReservation.Application.Room.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Room;
public class GetAllEndpoint(IMediator mediator): BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll(Guid hotelId)
    {
        var result = await mediator.Send(new Query(hotelId));

        if (result.IsFailure)
            return HandleFailure(result, "An error occurred while retrieving the rooms.");

        return Ok(result.Value);
    }
}
