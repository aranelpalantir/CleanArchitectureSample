# Clean Architecture Sample Project

This project is designed following the principles of Clean Architecture. Below is a list of major components and libraries used in the project:

## Used Components

### General Components
- [AutoMapper](https://github.com/AutoMapper/AutoMapper) - Library for object-to-object mapping.
- [Bogus](https://github.com/bchavez/Bogus) - Library for generating fake data for testing purposes.
- [FluentValidation](https://github.com/FluentValidation/FluentValidation) - Library for defining validation rules.
- [MassTransit](https://masstransit-project.com/) - Library for event-driven communication in distributed systems.
- [MediatR](https://github.com/jbogard/MediatR) - Library for simple and effective event handling within the application.

### Database and ORM
- [Microsoft.EntityFrameworkCore](https://docs.microsoft.com/en-us/ef/core/) - Entity Framework Core ORM (Object-Relational Mapping) library.
- [Microsoft.EntityFrameworkCore.SqlServer](https://docs.microsoft.com/en-us/ef/core/providers/sql-server/) - Entity Framework Core provider for SQL Server.

### Health Checks
- [AspNetCore.HealthChecks](https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks) - Library for monitoring application health.
- [AspNetCore.HealthChecks.Rabbitmq](https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks) - Health check for RabbitMQ.
- [AspNetCore.HealthChecks.Redis](https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks) - Health check for Redis.
- [AspNetCore.HealthChecks.SqlServer](https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks) - Health check for SQL Server.
- [AspNetCore.HealthChecks.UI](https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks) - Interface for visualizing health check results.
- [AspNetCore.HealthChecks.UI.InMemory.Storage](https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks) - Library for temporarily storing health check results in memory.

### Security and Identity Management
- [System.IdentityModel.Tokens.Jwt](https://github.com/AzureAD/azure-activedirectory-identitymodel-extensions-for-dotnet) - Library for creating and validating JSON Web Tokens (JWT).
- [Microsoft.AspNetCore.Authentication.JwtBearer](https://github.com/dotnet/aspnetcore) - ASP.NET Core authentication library for JWT-based authentication.
- [Microsoft.AspNetCore.Identity.EntityFrameworkCore](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-6.0&tabs=visual-studio) - Identity management with ASP.NET Core Identity.

### Testing
- [FluentAssertions](https://fluentassertions.com/) - Fluent API for asserting the results of unit tests.
- [Microsoft.AspNetCore.Mvc.Testing](https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-6.0) - Library for testing ASP.NET Core MVC applications.
- [Microsoft.NET.Test.Sdk](https://github.com/microsoft/vstest) - Test SDK for .NET projects.
- [Testcontainers.MsSql](https://github.com/HofmeisterAn/dotnet-testcontainers) - .NET library for managing Docker containers in integration tests.
- [xunit](https://xunit.net/) - Unit testing framework for .NET.
- [xunit.runner.visualstudio](https://xunit.net/) - xUnit test runner for Visual Studio.

### Logging
- [Serilog](https://serilog.net/) - Configurable and extendable .NET logging library.
- [Serilog.Extensions.Logging](https://github.com/serilog/serilog-extensions-logging) - Serilog provider for Microsoft.Extensions.Logging.

### Caching
- [StackExchange.Redis](https://github.com/StackExchange/StackExchange.Redis) - .NET library for Redis.

### API Documentation
- [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) - ASP.NET Core integration for Swagger documentation.
