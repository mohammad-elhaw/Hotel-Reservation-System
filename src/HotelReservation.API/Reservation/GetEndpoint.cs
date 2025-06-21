using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Reservation;
public class GetEndpoint(IMediator mediator) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> Get(Guid hotelId)
    {
        var result = await mediator.Send(
            new Application.Reservation.Queries.GetAll.Request(hotelId));

        if (result.IsSuccess && result.StatusCode == StatusCodes.Status404NotFound)
            return StatusCode(result.StatusCode, new
            {
                result.Value,
                result.Code
            });

        return result.IsSuccess
            ? StatusCode(result.StatusCode, result.Value)
            : HandleFailure(result, "Failed To Get Reservation");
    }

    [HttpGet("{reservationId}")]
    public async Task<IActionResult> GetById(Guid hotelId, Guid reservationId)
    {
        var result = await mediator.Send(
            new Application.Reservation.Queries.GetById.Request(hotelId, reservationId));
        
        return result.IsSuccess
            ? StatusCode(result.StatusCode, result.Value)
            : HandleFailure(result, "Failed To Get Reservation By Id");
    }
}
