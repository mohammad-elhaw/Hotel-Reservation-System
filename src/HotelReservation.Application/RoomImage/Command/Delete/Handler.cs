using HotelReservation.Application.RoomImage.Events;
using HotelReservation.Domain;
using HotelReservation.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HotelReservation.Application.RoomImage.Command.Delete;
public class Handler(
    Queries.RoomImage.GetById.IRepository getImageRepo,
    Infrastructure.Room.Image.Delete.IRepository deleteImageRepo,
    Infrastructure.Outbox.Add.IRepository outboxRepo) : IRequestHandler<Request, Result>
{
    public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
    {
        var imageResult = await getImageRepo.GetById(request.ImageId);
        if(imageResult.IsFailure) return Result.Failure(imageResult.Errors);

        deleteImageRepo.Delete(imageResult.Value!);
        int deleteResult = await deleteImageRepo.SaveChanges();

        if (deleteResult <= 0)
            return Result.Failure(new List<string>
            { "Failed to delete image from database." },
            StatusCodes.Status500InternalServerError);


        var domainEvent = new RoomImageDeleted(imageResult.Value!.Id, 
            imageResult.Value.ImageUrl);

        var outbox = new OutboxMessage(domainEvent);

        outboxRepo.Add(outbox);
        
        int outboxResult = await outboxRepo.SaveChanges();
        if(outboxResult <= 0)
            return Result.Failure(new List<string> 
            { "Failed to save outbox message." },
            StatusCodes.Status500InternalServerError);
        
        return Result.Success();
    }
}
