using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Amenity;
public class GetEndpoint(IMediator mediator) : BaseController
{
    [HttpGet("{amenityId}")]
    public async Task<IActionResult> GetById(Guid amenityId)
    {
        var result = 
            await mediator.Send(new Application.Amenity.Queries.GetById.Query(amenityId));

        return result.IsSuccess
            ? Ok(result.Value)
            : HandleFailure(result, "Failed to get amenity");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = 
            await mediator.Send(new Application.Amenity.Queries.GetAll.Query());

        return result.IsSuccess
            ? Ok(result.Value)
            : HandleFailure(result, "Failed to get amenities");
    }
}
