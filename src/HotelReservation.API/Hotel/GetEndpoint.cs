using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Hotel;
public class GetEndpoint(IMediator mediator) : BaseController
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await mediator.Send(
            new Application.Hotel.Queries.GetById.Query(id));

        return result.IsSuccess
            ? Ok(result.Value)
            : HandleFailure(result, "An error occurred while retrieving the hotel.");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(
            new Application.Hotel.Queries.GetAll.Query());

        return result.IsSuccess
            ? Ok(result.Value)
            : HandleFailure(result, "An error occurred while retrieving the hotels.");
    }

}
