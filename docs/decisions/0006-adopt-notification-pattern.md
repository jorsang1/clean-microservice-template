# Adopt notification pattern

* Status: Accepted
* Deciders: jorsang1
* Date: 2022-02-07

## Context and Problem Statement

It is not easy to decide where to implement validations and how to implement them.
  
About the `where`:  
We decided to check business rules as close to the domain as possible whenever possible to avoid duplication of validation logic.
But we also admit there could be some cases were it makes sense to validate only on higher layers as it is were you have the context available.

About the `how`:
We didn't want to go for an 'exception driven flow' therefore we adopt the notification pattern proposed by [Martin Fowler](https://martinfowler.com/articles/replaceThrowWithNotification.html).
  
Basically, what we do is to handle validations on the domain level and append the errors to the entity as they implement the `IValidatable` interface.  
Then, the Application layer is the one responsible to check the validation and throw a custom `ValidationException` with the errors.  
Then on the upper layer, the API, catches the exception and gets the errors and return the object with all the errors that occured.


## Considered Options

* We were considering the great information gathered in the following articles:
(https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/domain-model-layer-validations)  
(https://lostechies.com/jimmybogard/2009/02/15/validation-in-a-ddd-world/)  
(http://gorodinski.com/blog/2012/05/19/validation-in-domain-driven-design-ddd/)  

* Another idea to implement could be to use the 'Result' class from the library suggested by Nick Chapsas in the following video:
[Don't throw exceptions in C#. Do this instead](https://www.youtube.com/watch?v=a1ye9eGTB98)


## Decision Outcome

The outcome is some sort of middle-ground solution that tries to get the best of every world.


### Positive Consequences 

* Multiple validations can be gather at the same time by not throwing.

### Negative Consequences 

* Still some performance impact by using exception on the Application layer.


## Links 

* Back to [index](index.md)
