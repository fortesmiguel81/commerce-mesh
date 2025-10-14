using System.Text;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Infrastructure.Time;
using OrderService.SharedKernel;
using Microsoft.Extensions.Configuration;

namespace OrderService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection
        AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration) =>
        services
            .AddServices()
            .AddDatabase(configuration)
            .AddHealthChecks(configuration)
            .AddAuthenticationInternal(configuration)
            .AddAuthorizationInternal();

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        // Add other infrastructure services here

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Database");

        // Here you would typically add your DbContext, e.g.:

        return services;
    }

    private static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        // Here you would typically add health checks, e.g.:

        return services;
    }

    private static IServiceCollection AddAuthenticationInternal(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Here you would typically configure authentication, e.g., JWT Bearer, etc.

        return services;
    }

    private static IServiceCollection AddAuthorizationInternal(this IServiceCollection services)
    {
        // Here you would typically configure authorization policies.

        return services;
    }
}
