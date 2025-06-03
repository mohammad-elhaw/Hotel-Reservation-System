using HotelReservation.Application;
using Microsoft.Extensions.DependencyInjection;

namespace HotelReservation.API;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddApplication();
        return services;
    }
}
