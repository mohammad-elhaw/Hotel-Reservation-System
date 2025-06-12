using CloudinaryDotNet;
using HotelReservation.Application.RoomImage.Outbox;
using HotelReservation.Infrastructure;
using HotelReservation.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HotelReservation.Application;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        services.AddSingleton<ICloudinary, Cloudinary>(provider =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            return new Cloudinary(new Account(
                configuration["Cloudinary:CloudName"],
                configuration["Cloudinary:ApiKey"],
                configuration["Cloudinary:ApiSecret"]));
        });

        services.AddScoped<CloudImage.Contracts.IAdd , CloudImage.Add>();
        services.AddScoped<CloudImage.Contracts.IDelete, CloudImage.Delete>();


        #region Host Services
        services.AddHostedService<OutboxDispatcher>();
        #endregion

        services.AddInfrastructure();
        services.AddQueries();
        return services;
    }

}
