using HotelReservation.Application.Contracts;
using HotelReservation.Infrastructure;
using HotelReservation.Queries;
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
        services.AddScoped<IReservationService, ReservationService>();

        services.AddInfrastructure();
        services.AddQueries();
        return services;
    }

}
