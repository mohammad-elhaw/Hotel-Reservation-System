using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace HotelReservation.Queries;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddQueries(this IServiceCollection services)
    {
        services.AddTransient<IDbConnection>(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            return new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        });

        #region Hotel
        services.AddScoped<Hotel.GetById.IRepository, Hotel.GetById.Repository>();
        services.AddScoped<Hotel.GetAll.IRepository, Hotel.GetAll.Repository>();
        #endregion

        #region Room
        services.AddScoped<Room.GetById.IRepository, Room.GetById.Repository>();
        services.AddScoped<Room.GetAll.IRepository, Room.GetAll.Repository>();
        #endregion

        return services;
    }
}
