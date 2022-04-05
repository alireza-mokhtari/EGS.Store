# EGS Book Store

## Architecture

Briefly, Clean Architecture is a popular style of source organization that can be easily read and maintained by other developers. This way of organization also relies on abstractions instead of concrete types which leads to de-coupling concerns and more testability. Moreover, Repository Pattern is used to prevent direct manipulation of DbContext.
The project contains below layers:

- **Domain:** Consists core entities and definitions that business logic and other parts of the system depends on them.
- **Application:** This layer implements the business logic of the system.
- **Infrastructure:** As the architecture follows the inversion of control, infrastructure layer that contains required assets to provide data (from database or external services) depends on Application and Domain layers.
- **API:** This layer is also depends on Application layer and acts as Presentation layer of Clean Architecture. It exposes REST APIs using specified configurations.

**\*\*** All the layers utilize the most recent version of _.Net 6_.

## Technologies

Thanks to open-source contributions, below well-known libraries and frameworks are being used in this project:

1. [**FluentValidation:**](https://github.com/FluentValidation/FluentValidation)This lightweight package uses a fluent interface and lambda expression for building validation rules. The package has been used to decouple validation rules and models.
2. [**Swashbuckle:**](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)This tool generates documentation and communication interface for APIs automatically. It displays the swagger-ui and eliminates the need of using 3rd parties like postman. Besides the documentation is always sync with APIs.
3. [**Mapster:**](https://github.com/MapsterMapper/Mapster)The tool is a high performance object mapper to reduce cost of maintenance. The library brings at least 2.5 times faster mapping process than AutoMapper.
4. [**MediatR:**](https://github.com/jbogard/MediatR)It&#39;s a simple but powerful implementation of mediator pattern. Mediator pattern itself helps to eliminate direct object interactions which leads to maintainable and more testable code.
5. [**Entity Framework Core:**](https://github.com/dotnet/efcore) It enables object-relational mapping to work with relational data using domain objects. This ORM can reduces the cost of maintenance while it can effectively generate and executes queries against a relation database.
6. [**ASP.Net Core Identity:**](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-6.0) It&#39;s a membership system supports role-base, claim-based and policy-based authorization styles. The library is a plug and play user management system that can work alongside other 3rd parties seamlessly.
