# Clean microservice template
This is a project template to create microservices in .NET 6 based on the Clean architecture approach (or Onion, or Hexagonal, or Ports and adapters).  


## Main layers

### API
The API is generated using the new minimal api approach as the microservice shouldn't be that big

### Application
The application layer contains all queries and commands so you can see the operations this microservice is doing in a quick sight.  

### Domain
The domain layer uses records to represent our aggragte roots and value objects.

### Infrastructure
Only the database layer is implemented using an in memory repository.


## TODO

- ADR decisions should be reachable from the solution explorer... how?
- Add unit testing
  > And maybe even some functional or integration tests.
  > This video about [clean testing](https://www.youtube.com/watch?v=hV43fiHYBb4) can inspirational.
- Add identity handling
  > Even though it could be unnecessary for some microservices it's a very common problem that is nice to have it solved, and the one who don't need it can easily remove it. (or it could even be parametrizable)
- Shall we add `ID` value object for entities?
  > https://andrewlock.net/using-strongly-typed-entity-ids-to-avoid-primitive-obsession-part-1/
- What about the metrics? 
  > AppMetrics doesn't seem to be updated for .NET 6.    
  > There is this option from MS [Announcing dotnet monitor in .NET 6](https://devblogs.microsoft.com/dotnet/announcing-dotnet-monitor-in-net-6/)   
- Improve dockerfile offered by adding some security advices
- Add logging.
  > Scoped logging, propose the right places on where and what to log (Request filters, mediator behaviors, etc)
- Behavior pipelines for exception handling, maybe?
- Add more examples for Value objects
- Ubiquitous language. 
  > Based on [this article](https://blog.ndepend.com/checking-ddd-ubiquitous-language-with-ndepend/), they idea could be to have a dictionary of terms and actions defined and then use those keys to check the entities, properties and methods on the domain layer on a unit test so we make sure we have a definition and our code follows what is understandable in the bounded context.  
  Continuing on this idea the terms with descriptions could be also available via endpoint so it can be queried or published or whatever.
- More explanations on the readme or other docs
- Validations
 > Flow validations, errors on permission, or system/network errors like HTTP communication issues should be validated in application/web layer.


## Features

### DDD
The template is prepared to do **D**omain **D**riven **Design** by providing a 'Domain' layer ready to host you aggregate roots, value names, and so on in an structured way.

### CQRS
The template is also ready to separate your **Queries** and **Commands** in the application layer.   
I know CQRS can be taken further but this at least could be a starter for the microservice you are creating.   

### Mapster
Uses mapster as the default mapper but you can allways write your own or use another one.

### MediatR
The controllers uses mediator in order to call the application layer.

### FluentValidation
The domain validation is done using FuentValidation.

### Fluent Assertions
Unit testing is done using Fluent Assertions.

### Notification pattern
We try to avoid exception driven development therefore we apply in our own way the notification pattern proposed by [Martin Fowler](https://martinfowler.com/articles/replaceThrowWithNotification.html).  
Basically, what we do is to handle validations on the domain level and append the errors to the entity as they implement the `IValidatable` interface.   
Then, the Application layer is the one responsible to check the validation and throw a custom `ValidationException` with the errors.   
Then on the upper layer, the API, catches the exception and gets the errors and return the object with all the errors that occured.   

### ADR
ADR stands for **A**rchitectural **D**ecision **R**ecords.    
Please refer to [ADR.md](ADR.md) to know more about it.  
You can quickly take a look by exploring the [index](src/docs/decisions/index.md)   


## Solution structure
Main folders:  
    
* Domain => The domain layer uses records to represent our aggragte roots and value objects.
* Application => The application layer contains all queries and commands so you can see the operations this microservice is doing in a quick sight.   
This layer is organized with a first level of folders containing each "entity" and then another level with a folder for the queries, and a folder for the commands, and then the next level is another folder for the especific Query or Command where you can find everything related to this operation.   
That's the same convention as in the Jason Clean Architecture template, but in this case I've prefered to keep one file per class approach instead of having the Query and the handler in the same file.   
* Api => The API is generated using the new minimal api approach as the microservice shouldn't be that big.
* Infrastructure => Only the database layer is implemented using an in memory repository.

### Build Solution
* Build => Build Solution

### Run Tests
* Test => Windows => Test Explorer => Run All

### Run Tests with Command Prompt/Windows PowerShell
* Open Folder in File Explorer
* Open Command Prompt/Windows PowerShell
* Run "dotnet vstest *Tests*.dll"


## How to contribute
TODO