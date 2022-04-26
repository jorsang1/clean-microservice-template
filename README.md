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
- Shall we add `ID` value object for entities?
  > https://andrewlock.net/using-strongly-typed-entity-ids-to-avoid-primitive-obsession-part-1/
- We give up with mapster for Value objects. Moved to a custom mapper. See method GetAll from repository.
- What about the metrics? 
  > AppMetrics doesn't seem to be updated for .NET 6.    
  > There is this option from MS [Announcing dotnet monitor in .NET 6](https://devblogs.microsoft.com/dotnet/announcing-dotnet-monitor-in-net-6/)   
- Nullability of types must not be ignored, warnings should be errors
This will cause more code, more conditionals and more verbosity, but the prize is null safety everywhere. Also, `!` operator shall be forbidden. We must either go all in or let's not at all.
- Records instead of classes where useful and applicable
- Global usings where useful
- Hermetisation (everything `private` instead of `public` by default)


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