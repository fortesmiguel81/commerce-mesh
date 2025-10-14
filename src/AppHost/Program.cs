IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.OrderService_Api>("order-service-api");

builder.Build().Run();
