using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelReservation.Infrastructure;
public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<HotelReservationDbContext>((sp, options)=>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        #region Hotel
        services.AddScoped<Hotel.Add.IRepository, Hotel.Add.Repository>();
        services.AddScoped<Hotel.Update.IRepository, Hotel.Update.Repository>();
        services.AddScoped<Hotel.Delete.IRepository, Hotel.Delete.Repository>();
        #endregion

        #region Room
        services.AddScoped<Room.Add.IRepository, Room.Add.Repository>();
        services.AddScoped<Room.Update.IRepository, Room.Update.Repository>();
        services.AddScoped<Room.Delete.IRepository, Room.Delete.Repository>();
        #endregion

        #region RoomImage
        services.AddScoped<Room.Image.Add.IRepository, Room.Image.Add.Repository>();
        services.AddScoped<Room.Image.Delete.IRepository, Room.Image.Delete.Repository>();
        #endregion

        #region Outbox
        services.AddScoped<Outbox.Add.IRepository, Outbox.Add.Repository>();
        services.AddScoped<Outbox.GetPending.IRepository, Outbox.GetPending.Repository>();
        services.AddScoped<Outbox.IncrementAttempts.IRepository, Outbox.IncrementAttempts.Repository>();
        services.AddScoped<Outbox.MarkProcessed.IRepository, Outbox.MarkProcessed.Repository>();
        services.AddScoped<Outbox.UnitOfWork.IRepository, Outbox.UnitOfWork.Repository>();
        #endregion

        #region Amenity
        services.AddScoped<Amenity.Add.IRepository, Amenity.Add.Repository>();
        services.AddScoped<Amenity.Delete.IRepository, Amenity.Delete.Repository>();
        services.AddScoped<Amenity.Update.IRepository, Amenity.Update.Repository>();
        #endregion

        #region RoomAmenity
        services.AddScoped<RoomAmenity.Add.IRepository, RoomAmenity.Add.Repository>();
        services.AddScoped<RoomAmenity.Delete.IRepository, RoomAmenity.Delete.Repository>();
        #endregion

        #region Reservation
        services.AddScoped<Reservation.Add.IRepository, Reservation.Add.Repository>();
        services.AddScoped<Reservation.Delete.IRepository, Reservation.Delete.Repository>();
        #endregion

        #region UnitOfWork
        services.AddScoped<UnitOfWork.IRepository, UnitOfWork.Repository>();
        #endregion

        return services;
    }
}
