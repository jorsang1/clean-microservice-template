# Return a result object indicating success or failure to better control the flow

* Status: TO-DO
* Deciders: jorsang1
* Date: 2023-05-20

## Context and Problem Statement

When throwing exceptions we break the flow and therefore there is a lack of control of what is going on.  
Using exceptions for business validation or errors are not really exceptions so we should separate this two cases.
   

## Considered Options

* [Jos.Result](https://github.com/joseftw/jos.result)
* [FluentResults](https://github.com/altmann/FluentResults)
* OneOf
* Custom implementation

## Decision Outcome

Chosen option: "[FluentResults]", because we don't want to maintain another project and this nuget give us enough
flexibility.

### Positive Consequences 

* Makes all possible errors explicit and well-communicated
* Much better troubleshooting
* Better separation of the kind of error that can happen in the application
* Significantly reduces error handling code duplication

### Negative Consequences 

* Takes a fair bit of work to setup and use.


## Links 

* Back to [index](index.md)
