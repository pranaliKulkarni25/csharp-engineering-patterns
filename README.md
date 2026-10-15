# C# Engineering Patterns

Practical, well‑commented C# examples that demonstrate engineering and design patterns (starting with the SOLID principles). Each example is an executable demo with explanations, code samples, and a focused README so you can quickly learn the pattern and run hands‑on samples.

## Stack
- Language(s): C#
- Framework / runtime: .NET 6+ (ASP.NET Core minimal / Web API style)
- Notable libraries: ASP.NET Core, Microsoft.Extensions.DependencyInjection, Swagger (OpenAPI)

## What’s included
This repository is organized as a set of small projects, each demonstrating a single concept or pattern with runnable code and documentation.

Example modules:
- SOLID.S.SingleResponsibility/… - Demonstrates the Single Responsibility Principle with an Order processing demo (controllers, services, models and sample requests).  
  Each SOLID example also contains controllers for related principles: Liskov Substitution, Interface Segregation, Dependency Inversion, Open/Closed, etc.

## Repository layout


How it fits together:
- Program.cs wires up example services and controllers using DI and exposes a small Web API for sending sample requests.
- Controllers accept demo payloads, invoke the demo services and return results.
- Services implement focused responsibilities so you can see how responsibilities are separated and how to test/replace components.

## Quickstart - run a demo
Prerequisites:
- .NET 6 SDK or newer installed (dotnet --version >= 6.0)

From the repository root:
```bash
# restore and build
dotnet restore
dotnet build

# run the Single Responsibility demo project
dotnet run --project SOLID.S.SingleResponsibility/SOLID.S.SingleResponsibility/SOLID.Principles.Demo.csproj

Open the Swagger UI (when running in Development):
https://localhost:{port}/swagger

Example: call the Single Responsibility endpoint
Endpoint: POST /api/SingleResponsibility

Sample request body:
{
  "id": 1,
  "customerEmail": "john@example.com",
  "customerName": "John Doe",
  "items": [
    { "productName": "Laptop", "quantity": 1, "unitPrice": 999.99 },
    { "productName": "Mouse", "quantity": 2, "unitPrice": 29.99 }
  ]
}

cURL:
curl -k -X POST "https://localhost:{port}/api/SingleResponsibility" \
  -H "Content-Type: application/json" \
  -d '{ "id": 1, "customerEmail": "john@example.com", "customerName": "John Doe", "items":[{"productName":"Laptop","quantity":1,"unitPrice":999.99},{"productName":"Mouse","quantity":2,"unitPrice":29.99}] }'

What the demo shows:

- OrderService calculates totals and updates order status.
- Separate classes handle persistence (OrderRepository), email sending (EmailService), and invoice generation (InvoiceService) - illustrating Single Responsibility and making unit testing feasible.
