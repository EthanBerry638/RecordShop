# RecordShop API 💿

API for managing a record shop inventory

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
2. Set environment variables in launchSettings.json to 'Development'
3. Run dotnet run
4. Open https://localhost:7091/swagger to view and test endpoints
*Note: Data is in memory and will seed automatically on application restart*

Alternatively, you could set environment variables to Production and use a tool like Postman to test the endpoints

## Roadmap 🛣️

Refactor to use DTOs for GET endpoints

Relational Database Schema: Genres, Artists, Users

JWT Authentication
