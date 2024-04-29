# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en), and this project adheres to [Semantic Versioning](https://semver.org) 

<!--
## UNRELEASED
### Added
### Changed
### Deprecated
### Removed
### Fixed
### Security
-->

## 2.0.0
### Changed
- Update to .NET 8 and upgrade all nuget packages versions.

## 1.1.0
### Added
- Add `ConsoleLogging` or `JsonConsoleLogging` depending on settings. 
> So you can see normal logs in local while printing json logs in your console ready to ship out of your container with FileBeat or similar.
- Added 'Polly' policy for *retrials* and *circuit breaker* that you can configure using settings.
> Check the integration tests for the policies to see how it makes sure it does what you want.
- Added 'FluentResults'
> Control the flow using `Results<T>` instead of overusing Exceptions. Use exceptions only for the exceptional.
- Adding generic error handler with problem details.
> When something exceptional happens we handle it in a central point and return a *problem details*
### Changed
- Update versions on all nuget packages. (fix issues with migration of new MediatR and OpenTelemetry)

## 1.0.0
- Initial version of the template.