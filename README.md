# CarInventory Project

This project is organized to efficiently manage car inventories using a clean architecture approach.

## Project Structure

### CarInventory.Api
- **Controllers**
  - `CarController.cs`: Manages car operations.
  - `ThirdPartyIntegrationController.cs`: Handles third-party integrations.
- **Configurations**
  - `appsettings.json`: Application settings.
  - `Program.cs`: Main entry point.

### CarInventory.Application
- **Core Logic**
  - **Behaviors**: Pipeline behaviors (e.g., validation, logging).
  - **Commands**: Encapsulate actions.
  - **Queries**: Fetch data.
  - **Dtos**: Data transfer objects.
  - **Mappings**: Object mappings.
  - **Validators**: Input validation.

### CarInventory.Domain
- **Business Logic**
  - **Entities**: Core entities.
  - **Interfaces**: Repository interfaces.
  - **Exceptions**: Custom exceptions.

### CarInventory.Infrastructure
- **Data Access**
  - **Data**: Data context and configurations.
  - **Repositories**: Data access implementations.
  - **HealthChecks**: Application health checks.
  - **Migrations**: Database migrations.
  - **Middleware**: Custom middleware.

### Tests
- **Unit and Integration Tests**: Ensure functionality and reliability.

## Features

### Car Management
- Add new cars to the inventory (Done)
- Update car details (Done)
- Delete cars from the inventory (Done)
- View car details (Done)

### Inventory Management
- View list of all cars (TODO)
- Search and filter cars by various criteria (make, model, year, price, etc.) (TODO)
- Sort cars by different parameters (price, date added, etc.) (TODO)

### User Authentication and Authorization
- User registration and login (TODO)
- Role-based access control (e.g., admin, sales, viewer) (TODO)

### Sales Management 
- Record sales transactions (TODO)
- View sales history (TODO)
- Generate sales reports (TODO)

### Reporting and Analytics
- Generate reports on inventory status (TODO)
- Generate sales and revenue reports (TODO)
- Analytics dashboard for quick insights (TODO)

### Customer Management
- Add and manage customer details (TODO)
- View customer purchase history (TODO)

### Notifications
- Email or SMS notifications for inventory updates, sales, etc. (TODO)

## Architecture and Design Patterns
- **Clean Architecture**: Separation of concerns into distinct layers.
- **CQRS (Command Query Responsibility Segregation)**: Separate models for reading (queries) and writing (commands).
- **Repository Pattern**: Abstract data access logic from business logic.
- **Unit of Work**: Manage transactions across multiple repositories.
- **Logging**: Serilog for structured logging.
- **Validation**: FluentValidation for input validation.
- **Resilience**: Polly for handling transient faults.
- **Health Checks**: Implement health checks to monitor system status.
- **Swagger/OpenAPI**: For API documentation and testing.
- **EntityFrameworkCore**: For working with databases using the Entity Framework ORM.
- **MediatR**: For implementing the mediator pattern, which helps in decoupling the request/response logic.
- **AutoMapper**: For object-to-object mapping, which helps in simplifying the mapping code between DTOs and entities.
- **AspNetCoreRateLimit**: For implementing rate limiting in API.
- **API Versioning**: Use API versioning to manage changes and ensure backward compatibility.
