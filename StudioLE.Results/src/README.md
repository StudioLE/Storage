## About

A basic implementation of a railway oriented programming pattern with Success and Failure paths for a return value.

**As intriguing as this approach is it goes against the conventions of .NET.**

A better approach is to use the [nullable context](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/nullable-reference-types), return null on failure, and pass any warnings or errors to an ILogger defined by [dependency injection](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection).

If your code explicitly needs to read the warning or error messages then [CacheLogger](../../StudioLE.Extensions.Logging.Cache/src) may be a better approach.

## Acknowledgements
- Inspired by [Ardalis.Result](https://github.com/ardalis/Result)
