using HotelReservation.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API;
[ApiController]
public abstract class BaseController : ControllerBase
{
    protected IActionResult HandleFailure<T>(Result<T> result, string message)
    {
        return StatusCode(result.StatusCode, new ErrorResponse
        {
            ErrorCode = result.StatusCode,
            Message = message,
            Errors = result.Errors
        });
    }

    protected IActionResult HandleFailure(Result result, string message)
    {
        return StatusCode(result.StatusCode, new ErrorResponse
        {
            ErrorCode = result.StatusCode,
            Message = message,
            Errors = result.Errors
        });
    }

}
