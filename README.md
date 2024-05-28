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
