using HotelReservation.Application.CloudImage.Contracts;
using HotelReservation.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HotelReservation.Application.RoomImage.Command.Add;
public class Handler(
    Queries.Room.GetById.IRepository roomRepo,
    Queries.Hotel.GetById.IRepository hotelRepo,
    Infrastructure.Room.Image.Add.IRepository roomImageRepo,
    IAdd cloudImageService) : IRequestHandler<Request, Result>
{
    public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
    {
        // find the hotel
        var hotelResult = await hotelRepo.GetById(request.HotelId);
        if(hotelResult.IsFailure)
            return Result.Failure(hotelResult.Errors, hotelResult.StatusCode);

        // find the room
        var roomResult = await roomRepo.GetById(request.HotelId, request.RoomId);
        if(roomResult.IsFailure)
            return Result.Failure(roomResult.Errors, roomResult.StatusCode);

        List<Domain.Entities.RoomImage> uploadedImages = new();
        foreach (var image in request.Images)
        {
            var uploadResult = await cloudImageService.UploadImage(
                image, $"hotels/{request.HotelId}/rooms/{request.RoomId}");
            if (uploadResult.IsFailure)
                return Result.Failure(uploadResult.Errors, uploadResult.StatusCode);

            var roomImage = new Domain.Entities.RoomImage
            {
                ImageUrl = uploadResult.Value!,
                RoomId = request.RoomId
            };

            uploadedImages.Add(roomImage);
        }

        roomImageRepo.Add(request.RoomId, uploadedImages);
        int savingResult = await roomImageRepo.SaveChanges();
    
        if(savingResult <= 0)
            return Result.Failure(new List<string> 
            { "Failed to save room images." },
            StatusCodes.Status500InternalServerError);

        return Result.Success();
    }
}
