using NUnit.Framework;
using StudioLE.Diagnostics;
using StudioLE.Diagnostics.NUnit;
using StudioLE.Verify;

namespace StudioLE.Extensions.Logging.Tests;

internal sealed class AnsiFormatBuilderTests
{
    private readonly IContext _context = new NUnitContext();

    [Test]
    public async Task AnsiFormatBuilder_WithForegroundColor([Values] AnsiColor foregroundColor)
    {
        // Arrange
        AnsiFormatBuilder builder = new AnsiFormatBuilder()
            .WithForegroundColor(foregroundColor);

        // Act
        // Assert
        await ActAndAssert(builder);
    }

    [Test]
    public async Task AnsiFormatBuilder_WithBrightForegroundColor([Values] AnsiColor foregroundColor)
    {
        // Arrange
        AnsiFormatBuilder builder = new AnsiFormatBuilder()
            .WithBrightForegroundColor(foregroundColor);

        // Act
        // Assert
        await ActAndAssert(builder);
    }

    [TestCase(AnsiColor.Red, AnsiColor.Black, AnsiStyle.None)]
    [TestCase(AnsiColor.Magenta, AnsiColor.White, AnsiStyle.Bold)]
    [TestCase(AnsiColor.Red, AnsiColor.Black, AnsiStyle.Italic)]
    public async Task AnsiFormatBuilder_Build(AnsiColor foregroundColor, AnsiColor backgroundColor, AnsiStyle style)
    {
        // Arrange
        AnsiFormatBuilder builder = new AnsiFormatBuilder()
            .WithForegroundColor(foregroundColor)
            .WithBackgroundColor(backgroundColor)
            .WithStyle(style);

        // Act
        // Assert
        await ActAndAssert(builder);
    }

    private async Task ActAndAssert(AnsiFormatBuilder builder)
    {
        // Act
        string output = builder.Build();

#if DEBUG
        // Preview
        Console.WriteLine(output + "Hello, world!");
#endif

        // Assert
        Assert.That(output, Is.Not.Empty);
        await _context.Verify(output);
    }
}
