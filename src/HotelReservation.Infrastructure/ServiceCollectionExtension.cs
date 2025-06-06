using HotelReservation.Domain.Contracts;
using HotelReservation.Infrastructure.Repositories;
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

        services.AddScoped<IRepositoryManager, RepositoryManager>();
        services.AddScoped<IReservationRepository, ReservationRepository>();

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

        return services;
    }
}
