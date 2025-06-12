using HotelReservation.Domain;
using Microsoft.AspNetCore.Http;

namespace HotelReservation.Application.CloudImage.Contracts;
public interface IAdd
{
    Task<Result<string>> UploadImage(IFormFile image, string folder);
}
