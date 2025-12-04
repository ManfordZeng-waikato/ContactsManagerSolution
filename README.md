# Contacts Manager – ASP.NET Core Clean Architecture CRUD System

The Contacts Manager solution is a full-stack ASP.NET Core MVC application that implements Clean Architecture with Entity Framework Core, ASP.NET Core Identity, and a comprehensive automated testing setup.  

The project is designed as a practical, end-to-end example of an enterprise-style .NET application, focusing on separation of concerns, testability, and maintainable code rather than quick prototypes.

---

## Project Overview

The system manages people (contacts) and countries through an MVC web interface with full CRUD capabilities, dynamic sorting and filtering, validation, authentication, and role-based authorization.

The solution is split into multiple projects:

- A UI project for the presentation layer.
- A Core project for domain models, DTOs and service contracts.
- An Infrastructure project for persistence and Identity integration.
- Three dedicated testing projects for services, controllers, and integration testing.

---

## Solution Structure

```text
ContactsManagerSolution.sln
│
├── ContactsManager.UI
│   ├── Controllers (MVC endpoints)
│   ├── Views (Razor pages)
│   ├── Filters (Action & Authorization filters)
│   └── Identity UI integration
│
├── ContactsManager.Core
│   ├── Domain Models
│   ├── DTOs
│   ├── Enums
│   └── Service Contracts (interfaces for business logic)
│
├── ContactsManager.Infrastructure
│   ├── ApplicationDbContext (EF Core)
│   ├── Migrations
│   ├── Repository implementations
│   ├── Identity configuration
│   └── Data access utilities
│
├── ContactsManager.ServiceTests
│   └── Unit tests for the service layer
│
├── ContactsManager.ControllerTests
│   └── Unit tests for MVC controllers
│
├── ContactsManager.IntegrationTests
│   └── Integration tests for services + EF Core context
│
└── .github
    └── (optional) CI configuration


## Core Features

### Contact Management
- Full CRUD operations for person records.
- Dynamic sorting across all columns.
- Multi-field search: name, email, date of birth, address, gender, country.
- Fuzzy search support (e.g., year-only search for date fields).
- Country dropdown generated from database using `SelectListItem`.
- Server-side validation with error reporting.
- Custom action filters to repopulate data and return proper views on validation failure.

### Country Management
- Create and list country records.
- Clean mapping between entities and DTOs.
- EF Core migrations used for schema creation.

---

## Authentication and Authorization

### Identity Integration
- ASP.NET Core Identity for user and role management.
- Login, logout, password hashing, and validation.
- Configurable password rules (digit, uppercase, lowercase, special character).
- Fully configured `UserManager`, `SignInManager`, and `RoleManager`.

### Role System
- Built-in **Admin** and **User** roles.
- Automatic creation of the Admin role for the first admin registration.
- Default assignment of the User role to normal registrations.

### Custom Authorization Filters
- Token-based authorization example using `TokenAuthorizationFilter`.
- Returns `401` or `403` based on cookie validation.
- Demonstrates custom security logic beyond Identity.


### Key Design Principles
- Separation of Concerns across UI, business, and persistence layers.
- Service Layer Pattern for business logic abstraction.
- Repository Pattern for accessing and querying data.
- DTO usage for controlled data transfer.
- Dependency Injection for loose coupling and testability.
- Custom Filters for validation and authorization.
- LINQ and EF Core for expressive data querying.

---

## Testing Strategy

The solution uses three dedicated test projects to validate the entire application stack.

### Service Tests (`ContactsManager.ServiceTests`)
- Unit tests for business logic.
- Mapping and validation behavior.
- Repository calls abstracted through mocking.

### Controller Tests (`ContactsManager.ControllerTests`)
- Verification of action results, redirects, and returned views.
- Testing ModelState behavior.
- Ensuring correct handling of invalid input.

### Integration Tests (`ContactsManager.IntegrationTests`)
- Tests using EF Core with an in-memory or test database.
- Validation of repository + DbContext + service interaction.
- Ensures that the layers work together correctly.

---

## Technologies Used

### Backend
- ASP.NET Core MVC
- C#
- Entity Framework Core (Code-First)
- ASP.NET Core Identity

### Data Access
- SQL Server
- EF Core Migrations
- LINQ Querying

### Testing
- xUnit
- Moq
- AutoFixture
- FluentAssertions

### Tooling
- Visual Studio 2022
- NuGet Package Restore
- Git / GitHub

---

## Learning Outcomes

Through this solution, the project demonstrates:

- How to structure an ASP.NET Core application using Clean Architecture.
- Building a layered system with clear separation of UI, business, and persistence logic.
- Implementing EF Core with migrations and LINQ.
- Integrating and customizing ASP.NET Core Identity.
- Building custom action and authorization filters.
- Designing DTO pipelines to control data flow.
- Writing unit tests and integration tests for enterprise-grade .NET applications.
- Applying SOLID principles and best practices for maintainable and testable code.

