# Use fluent validations

* Status: proposed
* Deciders: jsellaresi
* Date: 2022-05-30

## Context and Problem Statement

Decide what''s the best way to validate in the code

## Decision Drivers <!-- optional -->

* readability
* easy to use and reuse

## Considered Options

* [FluentValidation]

## Decision Outcome

Chosen option: "[FluentValidation]", because privides a nice built-in validators and easy customizable validators.

### Positive Consequences <!-- optional -->

* Easy to implement
* Nice readability

### Negative Consequences <!-- optional -->

* As finally we are managing our custom error models, in some point we would use this and we will have not consistent pattern to validate.

## Pros and Cons of the Options 

### [Fluent Assertions]

[more information](https://docs.fluentvalidation.net/en/latest/#)

* Good, because best readability
* Good, because big community
* Good, because great api. Easy to use.
* Bad, because it''s not the same as in other projects in the past