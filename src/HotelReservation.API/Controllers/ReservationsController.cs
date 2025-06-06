using HotelReservation.Application.Contracts;
using HotelReservation.Application.DTOs.Booking;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController(IReservationService service) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateReservation([FromBody] ReservationRequestDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var result = await service.CreateReservation(dto);
        return Ok(result);
    }
}
