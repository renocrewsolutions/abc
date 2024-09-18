using RentalService.APIs;

namespace RentalService;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IBikesService, BikesService>();
        services.AddScoped<IPaymentsService, PaymentsService>();
        services.AddScoped<IRentalsService, RentalsService>();
        services.AddScoped<IUsersService, UsersService>();
    }
}
