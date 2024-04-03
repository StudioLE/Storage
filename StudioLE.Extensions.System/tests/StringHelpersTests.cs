using NUnit.Framework;

namespace StudioLE.Extensions.System.Tests;

internal sealed class StringHelpersTests
{
    [Test]
    public void StringHelpers_ReplaceLineEndings()
    {
        // Arrange
        string crlf = "Hello\r\nworld";
        string lf = "Hello\nworld";

        // Act
        string crlfReplaced = crlf.ReplaceWindowsLineEndings();
        string lfReplaced = lf.ReplaceWindowsLineEndings();

        // Assert
        Assert.That(crlfReplaced.Equals(lfReplaced));
        Assert.That(crlf.Equals(lf), Is.False);
    }
}
