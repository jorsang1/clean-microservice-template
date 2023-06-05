# Apply resilience policies

* Status: Implemented
* Deciders: jorsang1
* Date: 2023-05-20

## Context and Problem Statement

When we receive exceptions, in many cases we could try again and finish the call, or stop receiving new calls and give it time to recover.
We use retry policies and circuit breakers to retry and open the system if the percentage is reached.

## Considered Options

* [Polly](https://github.com/App-vNext/Polly)

## Decision Outcome

Chosen option: "[Polly]", because we want to have the maximum control of it and be flexible to configure it on our pipeline.  

### Positive Consequences 

* During the customer call, we can retry our calls and give the result if possible.
* When the maximum number of retries is reached, we can block new calls and give our system time to recover.

## Links 

* Back to [index](index.md)
