# Clean microservice template

This is a project template to create microservices in .NET 6 based on the Clean architecture approach (also known as Onion architecture, or Hexagonal architecture, or Ports and adapters).  
The goal is to provide a boilerplate for creating API's that enforces some good practices so it is easy to spin off new microservices but keeping a good level of quality on the solution.  
The solution is heavily inspired by the templated created by Jason Taylor.


## Features

- ADR
>ADR stands for **A**rchitectural **D**ecision **R**ecords.  
Please refer to [ADR.md](ADR.md) to know more about it.  
You can quickly take a look by exploring the [index](docs/decisions/index.md)

- DDD
>The template is prepared to do **D**omain **D**riven **D**esign by providing a 'Domain' layer ready to host you entities, value objects, and so on, in an structured way.

- CQRS
>The template is also ready to separate your **Queries** and **Commands** in the application layer using **MediatR**.  
[Link to the ADR decission](docs/decisions/0005-use-mediatr.md)

- Mapster
>Uses mapster as the default mapper but you can always write your own or use another one.  
[Link to the ADR decission](docs/decisions/0001-use-mapster.md)

- Validation
>The domain validation is done using *FluentValidation*
(*[Link to the ADR decission](docs/decisions/0003-use-fluent-validations.md)*) and following the *Notification pattern* (*[Link to the ADR decission](docs/decisions/0006-adopt-notification-pattern.md)*) suggested by Martin Fowler.

- Honeycomb testing
>The template comes with unit tests implemented as well as Integration tests using *xUnit*, *moq*, *Fluent Assertions* (*[Link to the ADR decission](docs/decisions/0002-use-fluent-assertions.md)*), and then everything is checked with mutation tests using *Striker.net* with the 'MultipleProjectRunner' plugin.

- Observability
>We're using [*Open Telemetry*](https://opentelemetry.io/docs/instrumentation/net/getting-started/) to add metrics and tracing instrumentation from the beginning!  
The template is ready to output the tracing information on the console as well as to use a grafana cloud agent as a OTLP collector but you can easily change it to use Jagger or Zipkin adding a new container on the docker compose.

- Others
>Scope logging, strong typed ID's to avoid primitive obsession, docker compose ready, just go ahead and take a look.



## Solution structure
Main folders:
* Domain => The domain layer contains your plain objects for entities and value objects as well as validators or any other core business logic you might have.
* Application => The application layer contains all queries and commands so you can see the operations this microservice is doing in a quick sight.  
This layer is organized with a first level of folders containing each "entity" and then another level with a folder for the queries, and a folder for the commands, and then the next level is another folder for the specific Query or Command where you can find everything related to this operation.  
That's the same convention as in the Jason Clean Architecture template, but in this case I've preferred to keep one file per class approach instead of having the Query and the handler in the same file.
* Api => The API is generated using the new minimal api approach as the microservice shouldn't be that big.
* Infrastructure => Only the database layer is implemented using an in memory repository.



## Test projects

Tests are separeted from the code in a diferent root folder and you can see them on the solution also under the 'tests' solution folder.

We provide the following tests projects:
- Application IntegrationTests  
The test project is testing the application layer with its real dependencies.
- Application UnitTests  
The test project is testing the application layer with mocked dependencies.
- Domain UnitTests  
  The test project is testing the domain layer.
- Infrastructure UnitTests  
  The test project is testing the infrastructure layer.


### Runing the project

**Build Solution**
* Build => Build Solution

**Run Tests**
* Test => Windows => Test Explorer => Run All

**Run Tests with Command Prompt/Windows PowerShell**
* Open Folder in File Explorer
* Open Command Prompt/Windows PowerShell
* Run `dotnet vstest *Tests*.dll`

**Run Mutation Tests with Stryker.net**
* Open terminal and run `dotnet tool restore` to install Stryker.net for the project.
* Navigate to root folder and run the powershell `.\RunAllSteps.ps1`

**Run the solution using docker compose**  
The solution has a `Dockerfile` and it is ready to be used with docker compose.

If you work with only WSL (Windows Subsystem for Linux) version 2 with docker installed.  
Then you just need to go to the path of the solution and run the following command:  
`docker compose up`  
The API should be available on [localhost:8080](http://localhost:8080)

If you are using 'Docker Desktop' then you need to enable the build on the `docker-compose` project by editing the properties on the solution and going to 'Configuration properties > Configuration' and checking the project.

## How to contribute
Everyone should be able to create a 'Pull Request' with a suggested change.  
Feel free to reach out if you need to discuss anything beforehand.  
:-)

**Some to-do's we think could be added:**

- Add identity handling
  > Even though it could be unnecessary for some microservices it's a very common problem that is nice to have it solved, and the one who don't need it can easily remove it. (or it could even be parametrizable)
- Ubiquitous language.
  > Based on [this article](https://blog.ndepend.com/checking-ddd-ubiquitous-language-with-ndepend/), they idea could be to have a dictionary of terms and actions defined and then use those keys to check the entities, properties and methods on the domain layer on a unit test so we make sure we have a definition and our code follows what is understandable in the bounded context.
  Continuing on this idea the terms with descriptions could be also available via endpoint so it can be queried or published or whatever.
- Apply ubiquitous language in domain.
  > Continuing on the previous point, in the domain, for example, the object `Product` has a generic method update, could be splited in three methods: `productUpdateTitleAndDescription`, `productUpdateSku`, `productUpdatePrice`.
