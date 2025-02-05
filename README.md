MarCorp.DemoBack
Enterprise .NET Core 9.0 Application – A Modular, Scalable Solution

Description
MarCorp.DemoBack is an enterprise-level application built using a layered architecture that clearly separates responsibilities. Developed on .NET Core 9.0, the project leverages modern technologies such as Dapper for efficient data access, AutoMapper for object mapping, FluentValidation for model validation, JWT for token-based authentication, and Swashbuckle for API documentation (Swagger/OpenAPI). The solution is organized into distinct layers—Application, Data, Domain, Services.WebApi, and Support—making it easier to maintain, test, and scale while also integrating with Docker and CI/CD pipelines via GitHub Actions.

Table of Contents:
Installation and Setup
Usage
Project Architecture
Key Technologies
Data Flow Example
Recommendations and Future Improvements
Contributing
License
Contact

Installation and Setup

Requirements
.NET 9.0 SDK
Git
Docker (optional, for containerization)
Visual Studio or VS Code

Installation Steps
Clone the Repository:
git clone https://github.com/yourusername/MarCorp.DemoBack.git
Navigate to the Project Directory:
cd MarCorp.DemoBack

Restore and Build the Solution:
dotnet restore
dotnet build
Configure the Environment:
Update appsettings.json with your connection strings and required parameters.
Use environment-specific configuration files (e.g., appsettings.Development.json for development).

Usage

Running the API
To start the web API, navigate to the Services.WebApi folder and run:
dotnet run --project Services.WebApi/Services.WebApi.csproj
The API will be available (e.g., at http://localhost:5000), and its interactive documentation can be accessed via Swagger at http://localhost:5000/swagger.

Running Tests
Currently, there is a test in UsersApplicationTest.cs. To run the tests, execute:
dotnet test
Note: It is recommended to expand the unit and integration test coverage to include additional components.

Project Architecture

The project is organized as follows:
MarCorp.DemoBack/
├── .github/                   # GitHub Actions configuration (CI/CD)
├── Application/               # Business logic and use cases
│   ├── DTO/                   # Data Transfer Objects
│   ├── Interface/             # Application service interfaces
│   ├── Main/                  # Service implementations
│   └── Validator/             # Model validations using FluentValidation
├── Data/                      # Data access layer
│   ├── Connections/           # Connection configurations (using Dapper)
│   ├── Interface/             # Repository interfaces
│   └── Repository/            # Repository implementations
├── Domain/                    # Core domain logic
│   ├── Core/                  # Domain entities and services
│   ├── Interface/             # Domain interfaces
│   └── Models/                # Database models/entities
├── Services.WebApi/           # Presentation layer (REST API)
│   ├── Controllers/           # HTTP endpoints
│   ├── Modules/               # Modular configurations (Authentication, Swagger)
│   └── Properties/            # Environment settings
└── Support/                   # Cross-cutting utilities
    ├── Common/                # Helpers and standardized responses
    ├── Logging/               # Logging implementation
    └── Mapper/                # AutoMapper configuration
    
Each layer has defined responsibilities:
Application: Orchestrates business logic, handles DTOs, and performs validations.
Data: Manages data access via Dapper, implementing the Repository pattern.
Domain: Contains core entities and business rules.
Services.WebApi: Exposes functionality through HTTP endpoints and configures modules such as JWT authentication and Swagger.
Support: Provides common utilities, centralized logging, and object mapping configurations.

Key Technologies
.NET 9.0: Core framework for the project.
Dapper: A lightweight micro-ORM for data access.
AutoMapper: Simplifies mapping between DTOs and domain entities.
FluentValidation: Provides declarative validation of models.
Swashbuckle (Swagger): Generates interactive API documentation.
JWT: Handles authentication via tokens.
xUnit/MSTest: Frameworks used for unit testing.

Data Flow Example
Request Reception:
The UsersController.cs (in the API layer) receives an HTTP request.
DTO Validation:
The UsersDTO.cs is validated using UsersDTOValidator.cs.
Application Logic:
The UsersApplication.cs service orchestrates the business process.
Data Access:
The UsersRepository.cs accesses the database via Dapper.
Domain Rules:
The UsersDomain.cs applies core business rules.
Object Mapping:
MappingsProfile.cs (located in Support/Mapper) transforms data between layers.

Recommendations and Future Improvements

Testing:
Expand unit and integration test coverage beyond the current tests in UsersApplicationTest.cs.

Security:
Ensure no hardcoded keys or secrets exist in appsettings.json.
Consider implementing role-based authorization.

Optimization:
Integrate caching mechanisms in the repositories.
Enhance centralized logging (e.g., using Serilog with Elasticsearch).

Documentation:
Ensure API endpoints are well-documented in Swagger.
Use XML comments in the code for comprehensive technical documentation.

Infrastructure & Resilience:
Separate environment configurations (e.g., appsettings.Development.json vs. production).
Add health check endpoints (e.g., /health, /metrics).
Implement resilience patterns (such as circuit breakers using Polly) and audit critical database changes.

Contributing

Contributions are welcome! To contribute:
Fork the repository.
Create a branch for your new feature or bug fix.
Submit a pull request describing your changes.
For detailed guidelines, please refer to the CONTRIBUTING.md file (if available).

License
This project is licensed under the MIT License.

Contact
For questions, suggestions, or support, please contact:
daniel.mar@iest.edu.mx
