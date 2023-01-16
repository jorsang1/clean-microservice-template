# Use MediatR

* Status: Accepted
* Deciders: jorsang1
* Date: 2022-02-07

## Context and Problem Statement

Implement CQRS is quite easy using the mediator pattern.  
   

## Considered Options

* [MediatR]
* Custom implementation

## Decision Outcome

Chosen option: "[MediatR]", because the API is well known by the devs community and because it also brings the behavior pipelines.  

### Positive Consequences 

* Easy to implement
* Easy to adopt by others
* Behavior pipelines

### Negative Consequences 

* IRequest for both queries and commands can be a wee bit misleading. A custom implementation could bring clear ICommand and IQuery interfaces.


## Links 

* Back to [index](index.md)
