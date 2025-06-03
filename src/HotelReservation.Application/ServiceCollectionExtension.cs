using HotelReservation.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace HotelReservation.Application;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddInfrastructure();
        return services;
    }

}
