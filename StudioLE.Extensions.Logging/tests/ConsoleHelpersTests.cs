using NUnit.Framework;
using StudioLE.Diagnostics;
using StudioLE.Diagnostics.NUnit;
using StudioLE.Verify;
using SystemConsole = System.Console;

namespace StudioLE.Extensions.Logging.Tests;

internal sealed class ConsoleHelpersTests
{
    private readonly IContext _context = new NUnitContext();

    [Test]
    public async Task ConsoleHelpers_CaptureConsoleOutput()
    {
        // Arrange
        Action action = () => SystemConsole.WriteLine("This is a standard output message.");

        // Act
        string output = ConsoleHelpers.CaptureConsoleOutput(action);

        // Assert
        Assert.That(output, Is.Not.Empty);
        await _context.Verify(output);
    }
}
