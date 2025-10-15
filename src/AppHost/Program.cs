IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

// Remove the problematic bind mount and use a named volume instead
IResourceBuilder<PostgresDatabaseResource> database = builder
    .AddPostgres("database")
    .WithPgAdmin()
    .AddDatabase("orders-db");

builder.AddProject<Projects.OrderService_Api>("order-service-api")
    .WithReference(database)
    .WaitFor(database);

builder.Build().Run();
