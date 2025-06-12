using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Room.Image.Endpoint;

[ApiController]
[Route("api/Hotels/{hotelId}/Rooms/{roomId}/images")]
public class BaseController : API.BaseController
{
}
