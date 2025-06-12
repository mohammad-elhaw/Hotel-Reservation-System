using HotelReservation.Application.RoomImage.Events;
using HotelReservation.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

namespace HotelReservation.Application.RoomImage.Outbox;
public class OutboxDispatcher(IServiceScopeFactory scopeFactory) : BackgroundService
{
    private const int MAX_RETRY = 5;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while(!stoppingToken.IsCancellationRequested)
        {
            using var scope = scopeFactory.CreateScope();
            var cloudDelete= scope.ServiceProvider.GetRequiredService<CloudImage.Contracts.IDelete>();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<Infrastructure.Outbox.UnitOfWork.IRepository>();


            var pendingMessages = await unitOfWork.GetPendingRepo.GetPending(MAX_RETRY);

            foreach(var msg in pendingMessages)
            {
                var eventType = Type.GetType(msg.Type);
                if (eventType is null) continue;

                var domainEvent = JsonSerializer.Deserialize(msg.Payload, eventType);

                var success = domainEvent switch
                {
                    RoomImageDeleted e => await cloudDelete.DeleteImage(e.Url),
                    _ => Result.Failure(new List<string> 
                    { "Unknown event type." }, StatusCodes.Status400BadRequest)
                };

                if (success.IsSuccess)
                    await unitOfWork.MarkProcessedRepo.MarkProcessed(msg.Id);
                else
                    await unitOfWork.IncrementAttemptsRepo.IncrementAttempts(msg.Id);
            }
            await unitOfWork.SaveChanges();
            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}
