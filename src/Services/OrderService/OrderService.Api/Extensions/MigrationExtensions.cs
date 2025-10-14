namespace OrderService.Api.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        // Here you can add code to apply database migrations if needed
        // For example, using Entity Framework Core:
        // using (var scope = app.ApplicationServices.CreateScope())
        // {
        //     var dbContext = scope.ServiceProvider.GetRequiredService<YourDbContext>();
        //     dbContext.Database.Migrate();
        // }
    }
}
