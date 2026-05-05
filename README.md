# RecordShop API 🎶

API for managing a record shop inventory

## Features 💿

Retrieve Inventory: Get all albums or search by specific ID

Error Handling: Global exception middleware

Testing: Unit and Integration tests for existing endpoints

Health Checks: Endpoint to monitor API and Database health

## Tech Stack 🛠️

Language: C# 11 / .NET 8
Database: EF Core and SQLite (In-Memory)
Testing: NUnit, Fluent Assertions and Moq

## Getting Started 📖
1. Clone the repository.
2. Run dotnet run.
3. Open https://localhost:7091/swagger to view and test endpoints.
*Note: Data is in memory and will seed automatically on application restart.*

## Roadmap 🛣️

Finish CRUD endpoints for albums
Refactor to use DTOs for GET endpoints
Refactor to use persistent database
Relational Database Schema: Genres, Artists, Users
JWT Authentication
