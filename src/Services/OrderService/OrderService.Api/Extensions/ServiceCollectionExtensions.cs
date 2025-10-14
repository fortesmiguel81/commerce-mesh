namespace OrderService.Api.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddSwaggerGenWithAuth(this IServiceCollection services)
    {
        services.AddSwaggerGen(
            // Here we can configure Swagger to include security definitions for authentication
            );

        return services;
    }
}
