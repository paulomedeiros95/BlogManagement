# BlogManagement
This project is an ASP.NET Core Web API designed to manage blog posts and their respective comments. The solution follows modern development practices, including layered architecture, dependency injection, logging, and unit testing.

No need to worry about the database setup. If no database is configured in the appsettings, the application will automatically fall back to an in-memory database.

If I had more time: 
- AutoMapper.
- CI/CD configs.
- Enviroments configs.
- Observability.
- Healthcheck improvemtns.

Technologies Used: 
- .NET 8.
-  Entity Framework Core.
-  SQL Server as the primary database.
-  InMemory Database as a fallback.
-  NUnit for unit testing.
-  NSubstitute for mocking.
- ILogger for structured logging.

Layered Architecture:
- API.
- Services.
- Infra.
- Domain.

