using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Reservation;
[Route("api/hotels/{hotelId}/reservations")]
public abstract class BaseController : API.BaseController
{
}
