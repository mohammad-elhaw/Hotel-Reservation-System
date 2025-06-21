using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace HotelReservation.Queries;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddQueries(this IServiceCollection services)
    {
        services.AddScoped<IDbConnection>(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            return new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        });

        #region Hotel
        services.AddScoped<Hotel.GetById.IRepository, Hotel.GetById.Repository>();
        services.AddScoped<Hotel.GetAll.IRepository, Hotel.GetAll.Repository>();
        services.AddScoped<Hotel.Exists.IRepository, Hotel.Exists.Repository>();
        #endregion

        #region Room
        services.AddScoped<Room.GetById.IRepository, Room.GetById.Repository>();
        services.AddScoped<Room.GetAll.IRepository, Room.GetAll.Repository>();
        services.AddScoped<Room.Exists.IRepository, Room.Exists.Repository>();
        services.AddScoped<Room.GetByIds.IRepository, Room.GetByIds.Repository>();
        #endregion

        #region RoomImage
        services.AddScoped<RoomImage.GetById.IRepository, RoomImage.GetById.Repository>();
        #endregion

        #region Amenity
        services.AddScoped<Amenity.GetById.IRepository, Amenity.GetById.Repository>();
        services.AddScoped<Amenity.GetAll.IRepository, Amenity.GetAll.Repository>();
        services.AddScoped<Amenity.Exists.IRepository, Amenity.Exists.Repository>();
        #endregion

        #region RoomAmenity
        services.AddScoped<RoomAmenity.NotExists.IRepository, RoomAmenity.NotExists.Repository>();
        services.AddScoped<RoomAmenity.GetAll.IRepository, RoomAmenity.GetAll.Repository>();
        services.AddScoped<RoomAmenity.GetById.IRepository, RoomAmenity.GetById.Repository>();
        services.AddScoped<RoomAmenity.Get.IRepository, RoomAmenity.Get.Repository>();
        #endregion

        #region Reservation
        services.AddScoped<Reservation.GetAll.IRepository, Reservation.GetAll.Repository>();
        services.AddScoped<Reservation.GetById.IRepository, Reservation.GetById.Repository>();
        #endregion

        return services;
    }
}
