using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.RoomAmenity;

[Route("api/rooms/{roomId}/amenities")]
public abstract class BaseController : API.BaseController
{
}
