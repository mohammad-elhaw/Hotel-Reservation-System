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

        return result.IsSuccess
            ? Ok(result.Value)
            : HandleFailure(result, "An error occurred while retrieving the rooms.");
    }
}
