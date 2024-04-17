## About

Abstractions of `StudioLE.Storage`,  a standardized approach for reading and writing files asynchronously.

File handling is inconsistent across the .NET ecosystem and the lack of a consistent asynchronous approach makes file handling in Blazor complicated.

- `System.IO` has methods for reading and writing files on a physical system which works for Blazor Server but is unsuitable for Blazor WebAssembly.

- `Microsoft.Extensions.FileProviders` has an abstract approach to reading files from various sources but the reliance on synchronous methods are not suitable for Blazor WebAssembly where file operations are typically asynchronous. The lack of writing files is also a significant limitation.

### Examples

- The [unit tests](../../StudioLE.Storage/tests) provide clear examples of how to use the library.
