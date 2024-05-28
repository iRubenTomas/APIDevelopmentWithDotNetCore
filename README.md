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
- **Create Car**: Add a new car to the inventory.
- **Get Cars**: Retrieve a list of all cars.
- **Update Car**: Update car details.
- **Delete Car**: Remove a car from the inventory.

## Architecture and Design Patterns
- **Clean Architecture**: Separation of concerns into distinct layers.
- **CQRS (Command Query Responsibility Segregation)**: Separate models for reading (queries) and writing (commands).
- **Repository Pattern**: Abstract data access logic from business logic.
- **Unit of Work**: Manage transactions across multiple repositories.
- **Logging**: Use Serilog for structured logging.
- **Validation**: Use FluentValidation for input validation.
- **Resilience**: Use Polly for handling transient faults.
- **Health Checks**: Implement health checks to monitor system status.
- **Swagger/OpenAPI**: Use Swagger for API documentation and testing.
- **Authentication/Authorization**: Use JWT or OAuth for securing endpoints.
