using NUnit.Framework;
using StudioLE.Diagnostics;
using StudioLE.Diagnostics.NUnit;
using StudioLE.Verify;

namespace StudioLE.Extensions.Logging.Tests;

internal sealed class AnsiHelpersTests
{
    private readonly IContext _context = new NUnitContext();

    [Test]
    public async Task AnsiHelpers_ReplaceEscapeSequences()
    {
        // Arrange
        const string input = """
           [38;5;15mUsing .NET Core SDK 8.0.100[0m
           [38;5;15mLoading assembly /e/Repos/C#/Core/StudioLE.Conversion/src/bin/Release/netstandard2.0/StudioLE.Conversion.dll[0m
           [38;5;15mProcessing StudioLE.Conversion[0m
           [38;5;11mwarning: InvalidCref: Invalid cref value "!:TResult" found in XML documentation comment .[0m
           [38;5;15mCreating output...[0m


           [38;5;11mBuild succeeded with warning.[0m

           [38;5;11m    1 warning(s)[0m
           [38;5;15m    0 error(s)[0m
           """;

        // Act
        string output = AnsiHelpers.ReplaceEscapeSequences(input);

        #if DEBUG
        // Preview
        Console.WriteLine(output);
        #endif

        // Assert
        Assert.That(output, Is.Not.Empty);
        await _context.Verify(output);
    }
}
