using NUnit.Framework;
using StudioLE.Storage.Paths;
using StudioLE.Storage.Tests.Resources;

namespace StudioLE.Storage.Tests.Paths;

[TestFixture]
internal class FileUriHelpersTests
{
    [TestCase("https://john.doe@www.example.com:123/forum/questions/?tag=networking&order=newest#top", false)]
    [TestCase("mailto:John.Doe@example.com", false)]
    [TestCase("tel:+1-816-555-1212", false)]
    [TestCase("file://localhost/c:/WINDOWS/clock.avi", true)]
    [TestCase("file:///c:/WINDOWS/clock.avi", true)]
    [TestCase("urn:oasis:names:specification:docbook:dtd:xml:4.1.2", false)]
    [TestCase("file://localhost/etc/fstab", true)]
    [TestCase("file:///etc/fstab", true)]
    [TestCase("file:/etc/fstab", true)]
    [TestCase("data:image/jpeg;base64,/9j/4AAQSkZJRgABAgAAZABkAAD", false)]
    public void IsFileUri(string uri, bool expected)
    {
        // Arrange
        // Act
        bool result = FileUriHelpers.IsFileUri(uri);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Exists()
    {
        // Arrange
        string path = Path.Combine(ExampleHelpers.DirectoryPath, ExampleHelpers.FileName);
        string absolutePath = Path.GetFullPath(path);
        string uri = FileUriHelpers.FromAbsolutePath(absolutePath);

        // Act
        bool result = FileUriHelpers.Exists(uri);

        // Assert
        Assert.That(result, Is.True);
    }

    [TestCase("/c/path/to/file.txt", "file:///c/path/to/file.txt")]
    [TestCase("C:/path/to/file.txt", "file:///C:/path/to/file.txt")]
    [TestCase(@"C:\path\to\file.txt", "file:///C:/path/to/file.txt")]
    public void FromAbsolutePath(string absolutePath, string expected)
    {
        // Arrange
        // Act
        string result = FileUriHelpers.FromAbsolutePath(absolutePath);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase("data:image/jpeg;base64,/9j/4AAQSkZJRgABAgAAZABkAAD", null)]
    [TestCase("file://localhost/etc/fstab", null)]
    [TestCase("file:///c/path/to/file.txt", "/c/path/to/file.txt")]
    [TestCase("file:///C:/path/to/file.txt", "C:/path/to/file.txt")]
    public void GetPath(string uri, string? expected)
    {
        // Arrange
        // Act
        string? result = FileUriHelpers.GetPath(uri);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }
}
