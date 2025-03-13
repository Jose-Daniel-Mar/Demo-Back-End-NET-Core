MarCorp.DemoBack Enterprise .NET Core 9.0 Application – Clean Architecture Solution

Description
MarCorp.DemoBack is an enterprise-grade application built following Clean Architecture principles, designed to maximize maintainability, scalability, and testability. Developed on .NET 9.0, it integrates modern technologies like Dapper for efficient data access, AutoMapper for object mapping, FluentValidation for model validation, JWT for authentication, and Swagger for API documentation. The solution is organized into independent layers (Core, Application, Infrastructure, and Presentation) with unidirectional dependencies, implementing advanced features like health checks, rate limiting, and real-time monitoring with WatchDog, and Support—making it easier to maintain, test, and scale while also integrating with Docker and CI/CD pipelines via GitHub Actions.

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
The API will be available (e.g., at http://localhost:xxxx), and its interactive documentation can be accessed via Swagger at http://localhost:xxxxx/swagger.

Access:
API: http://localhost:xxxxx

Swagger UI: 
http://localhost:xxxxx/swagger

Health Checks: 
https://localhost:xxxxx/healthchecks-ui#/healthchecks
http://localhost:xxxxx/health

WatchDog: 
http://localhost:xxxx/watchdog

Running Tests
Currently, there is a test in UsersApplicationTest.cs. To run the tests, execute:
dotnet test

Project Architecture

![image](https://github.com/user-attachments/assets/0ea94484-2d1f-4d6e-bc90-cb9e4b247731)

![image](https://github.com/user-attachments/assets/9c706a67-c2ec-49db-9e41-fc12c6a58bdf)


Key Modules (under Services.WebApi/Modules):

Authentication: JWT configuration
HealthCheck: Dependency monitoring
RateLimiter: Request throttling
WatchDog: Real-time monitoring
Validator: Centralized validations

Key Technologies
.NET 9.0
Dapper (High-performance data access)
JWT (Token-based authentication)
Swashbuckle (OpenAPI documentation)
AutoMapper (Object mapping)
FluentValidation (Declarative validations)
Redis (Distributed caching)
WatchDog (Real-time monitoring)
Polly (Resilience & retries - planned)

Data Flow
HTTP Request: Received by UsersController (Presentation)
Validation: UsersDTOValidator validates model (Application)
Use Case: UsersApplication executes business logic (Application)
Data Access: UsersRepository interacts with DB via Dapper (Infrastructure)
Domain Rules: Entities in UsersDomain apply core logic (Core)
Mapping: MappingsProfile transforms data between layers

Recommendations and Future Improvements

Testing:
Expand unit and integration test coverage beyond the current tests in UsersApplicationTest.cs.

Security:
Ensure no hardcoded keys or secrets exist in appsettings.json.
Consider implementing role-based authorization.

Optimization:
Integrate caching mechanisms in the repositories.

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
