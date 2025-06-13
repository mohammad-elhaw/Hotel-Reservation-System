using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Room.Image;

[ApiController]
[Route("api/hotels/{hotelId}/rooms/{roomId}/images")]
public class BaseController : API.BaseController
{
}
