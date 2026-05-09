# RecordShop API 💿

RESTful API for managing a record shop inventory

## Features 🎶

CRUD Endpoints for Albums

Error Handling: Global exception handling middleware

Testing: Unit and Integration tests for existing endpoints

Health Checks: Endpoint to monitor API and Database health

## Tech Stack 🛠️

Language: C# 11 / .NET 8

Database: EF Core, SQL Server (Production) and SQLite (Development)

Testing: NUnit, Fluent Assertions and Moq

## Getting Started 📖

1. Clone the repository
2. Run `dotnet run`
3. Open `https://localhost:7091/swagger` to view and test endpoints

NOTE: By default, the API runs in a `Development` environment using an in-memory SQLite database

**To use a persistent SQL Server database:**
1. Update `appsettings.Production.json` with your local SQL server connection string
2. Change the `ASPNETCORE_ENVIRONMENT` variable in `Properties/launchSettings.json` to `Production`
3. Run the application. EF Core will automatically apply the migration

NOTE: Both the in-memory SQLite database and the persistent SQL Server database will be seeded by default.
If you wish to change this, find `database.SeedData()` inside Program.cs and either move it to the `else` block or comment/delete it.

## Roadmap 🛣️

Relational endpoints e.g. Get Albums by Artist

JWT Authentication