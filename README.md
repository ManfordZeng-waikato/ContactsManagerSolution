Contacts Manager – ASP.NET Core Clean Architecture CRUD System

A full-stack ASP.NET Core MVC application implementing Clean Architecture, Entity Framework Core, ASP.NET Core Identity, and a comprehensive automated testing setup.
The project is designed as a practical learning solution to understand enterprise-grade software structure, domain layering, dependency injection, validation, database modeling, and maintainable coding practices.

📘 Project Overview

The Contacts Manager system allows users to manage people and country records through a structured MVC interface, with full support for CRUD operations, sorting, searching, validation, authentication, authorization, and a layered architecture that clearly separates concerns.
The solution also integrates multiple test projects to ensure correctness across services, controllers, and database interactions.

📂 Solution Structure
ContactsManagerSolution.sln
│
├── ContactsManager.UI
│   ├── MVC Controllers
│   ├── Views (Razor)
│   ├── Filters (Action & Authorization)
│   └── Identity UI Integration
│
├── ContactsManager.Core
│   ├── Domain Models
│   ├── DTOs
│   ├── Enums
│   └── Service Contracts (Interfaces)
│
├── ContactsManager.Infrastructure
│   ├── ApplicationDbContext (EF Core)
│   ├── Migrations
│   ├── Repository Implementations
│   ├── Identity Configuration
│   └── Data Access Utilities
│
├── ContactsManager.ServiceTests
│   └── Unit Tests for Service Layer
│
├── ContactsManager.ControllerTests
│   └── Unit Tests for MVC Controllers
│
├── ContactsManager.IntegrationTests
│   └── End-to-End Tests for Services + Real Database Context
│
└── .github
    └── (Optional) CI/CD configuration

✨ Key Features
Contact Management

Full CRUD operations for person records

Dynamic sorting for all fields

Multi-field search (Name, Email, Date of Birth, Address, Gender, Country)

Fuzzy search support (e.g., searching only a year “199”)

Dropdown population for countries using SelectList

Server-side validation with custom error reporting

Custom action filters to rehydrate ViewBag data on validation failure

Country Management

Create and list country records

Data pulled via service and repository layers

Clean mapping using DTOs and domain models

🔐 Authentication & Authorization
ASP.NET Core Identity Integration

Login, logout, password hashing, identity validation

Password complexity rules enforced

UserManager, SignInManager, RoleManager fully configured

Role System

Admin and User roles

Automatic creation of Admin role during first admin registration

Users automatically assigned “User” role unless specified otherwise

Custom Authorization Filters

Cookie-based access control using TokenAuthorizationFilter

Role-based logic to restrict operations

⚙️ Architecture & Design Principles
Clean Architecture / Onion Architecture

The project enforces strict separation between UI, Core, and Infrastructure:

UI Layer (Controllers, Views, Filters)
    ↓
Core Layer (Business interfaces, DTOs, Models)
    ↓
Infrastructure Layer (Repositories, DbContext, EF Core, Identity)

Patterns Used

Repository Pattern for abstraction of data access

Service Layer Pattern for business logic

DTOs for data transfer between layers

LINQ for database querying

Dependency Injection for decoupling and testability

Custom Filters for validation and authorization

🧪 Testing Strategy

The solution includes three dedicated testing projects, showcasing how to validate each layer of a real-world application.

1. Service Tests

Located in ContactsManager.ServiceTests

Unit tests for service logic

DTO conversions

Mocking repository interactions

2. Controller Tests

Located in ContactsManager.ControllerTests

Validation of action results

ModelState behavior

Redirects and view rendering

3. Integration Tests

Located in ContactsManager.IntegrationTests

EF Core InMemory or Test DB

Testing Repository + DbContext + Services end-to-end

Ensures correctness across all layers

🛠 Technologies Used
Backend

ASP.NET Core 8 MVC

C# 12

Entity Framework Core

ASP.NET Core Identity

Testing

xUnit

Moq

AutoFixture

FluentAssertions

Other Tools

Razor Views

LINQ

Dependency Injection

Visual Studio 2022

NuGet package restore (automatic)

📌 Learning Outcomes

This project demonstrates:

Proper use of Clean Architecture in ASP.NET Core

Strong separation of concerns and SOLID principles

Using EF Core for database modeling and querying

Implementing Identity for secure authentication

Custom Authorization and Action Filters

Managing DTO pipelines across layers

Writing maintainable and testable C# code

Unit and integration testing in enterprise-style .NET applications