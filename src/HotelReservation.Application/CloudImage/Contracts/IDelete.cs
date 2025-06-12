using HotelReservation.Domain;

namespace HotelReservation.Application.CloudImage.Contracts;
public interface IDelete
{
    Task<Result> DeleteImage(string imageUrl);
}
