# CommerceMesh

A microservices-based e-commerce platform built with .NET Aspire orchestration.

## Project Structure

```
CommerceMesh/
├── src/
│   ├── AppHost/                    # .NET Aspire orchestrator
│   ├── BuildingBlocks/             # Shared libraries
│   ├── Services/
│   │   ├── ApiGateway/
│   │   ├── CatalogService/
│   │   ├── OrderService/
│   │   ├── InventoryService/
│   │   ├── PaymentService.Java/    # Spring Boot
│   │   ├── NotificationService.Node/ # Node.js + TypeScript
│   │   └── AuthService/
│   └── Workers/
│       ├── OrderWorker/
│       └── AnalyticsWorker/
├── tests/
│   ├── UnitTests/
│   └── IntegrationTests/
└── README.md
```

## Services Overview

- **AppHost**: .NET Aspire orchestrator for managing the microservices
- **BuildingBlocks**: Shared libraries and common functionality
- **ApiGateway**: Entry point for client requests
- **CatalogService**: Product catalog management
- **OrderService**: Order processing and management
- **InventoryService**: Inventory tracking and management
- **PaymentService.Java**: Payment processing (Spring Boot)
- **NotificationService.Node**: Notification system (Node.js + TypeScript)
- **AuthService**: Authentication and authorization
- **OrderWorker**: Background order processing
- **AnalyticsWorker**: Analytics and reporting

## Getting Started

TODO: Add setup and running instructions

## Technologies

- .NET Aspire
- .NET Core
- Spring Boot (Java)
- Node.js + TypeScript
- Docker
