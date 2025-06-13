using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Room;

[Route("api/hotels/{hotelId}/rooms")]
public abstract class BaseController : API.BaseController
{
}
