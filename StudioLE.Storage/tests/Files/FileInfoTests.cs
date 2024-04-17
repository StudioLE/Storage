using Newtonsoft.Json;
using NUnit.Framework;
using StudioLE.Diagnostics;
using StudioLE.Diagnostics.NUnit;
using StudioLE.Extensions.System;
using StudioLE.Storage.Files;
using StudioLE.Verify;
using FileInfo = StudioLE.Storage.Files.FileInfo;

namespace StudioLE.Storage.Tests.Files;

internal sealed class FileInfoTests
{
    private readonly IContext _context = new NUnitContext();

    [Test]
    public void FileInfo_Serialization_Newtonsoft()
    {
        // Arrange
        JsonSerializerSettings settings = new();
        FileInfo file = new()
        {
            Name = "An example document",
            Description = "A description of the document.",
            MediaType = "text/plain",
            Location = "https://localhost/my/file.txt"
        };

        // Act
        string json = JsonConvert.SerializeObject(file, settings);
        IFileInfo? deserialized = JsonConvert.DeserializeObject<FileInfo>(json, settings);
        string json2 = JsonConvert.SerializeObject(deserialized, settings);

        // Assert
        Assert.Multiple(async () =>
        {
            await _context.Verify(json, json2);
            Assert.That(deserialized, Is.Not.Null, "Not null");
            Assert.That(deserialized?.Id, Is.EqualTo(file.Id), "Id");
            Assert.That(deserialized?.Name, Is.EqualTo(file.Name), "Name");
            Assert.That(deserialized?.Description, Is.EqualTo(file.Description), "Description");
            Assert.That(deserialized?.MediaType, Is.EqualTo(file.MediaType), "MediaType");
            Assert.That(deserialized?.Location, Is.EqualTo(file.Location), "Location");
        });
    }

    [TestCase("https://john.doe@www.example.com:123/forum/questions/?tag=networking&order=newest#top", false)]
    [TestCase("mailto:John.Doe@example.com", false)]
    [TestCase("tel:+1-816-555-1212", false)]
    [TestCase("file://localhost/c:/WINDOWS/clock.avi", false)]
    [TestCase("file:///c:/WINDOWS/clock.avi", false)]
    [TestCase("urn:oasis:names:specification:docbook:dtd:xml:4.1.2", false)]
    [TestCase("file://localhost/etc/fstab", false)]
    [TestCase("file:///etc/fstab", false)]
    [TestCase("file:/etc/fstab", false)]
    [TestCase("data:image/jpeg;base64,/9j/4AAQSkZJRgABAgAAZABkAAD", true)]
    public void FileInfoHelpers_IsInlineData(string location, bool expected)
    {
        // Arrange
        FileInfo file = new()
        {
            Location = location
        };

        // Act
        bool result = file.IsInlineData();

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase("https://john.doe@www.example.com:123/forum/questions/?tag=networking&order=newest#top", false)]
    [TestCase("mailto:John.Doe@example.com", false)]
    [TestCase("tel:+1-816-555-1212", false)]
    [TestCase("file://localhost/c:/WINDOWS/clock.avi", true)]
    [TestCase("file:///c:/WINDOWS/clock.avi", true)]
    [TestCase("urn:oasis:names:specification:docbook:dtd:xml:4.1.2", false)]
    [TestCase("file://localhost/etc/fstab", true)]
    [TestCase("file:///etc/fstab", true)]
    [TestCase("file:/etc/fstab", true)]
    public void FileInfoHelpers_IsLocalFile(string location, bool expected)
    {
        // Arrange
        FileInfo file = new()
        {
            Location = location
        };

        // Act
        bool result = file.IsPhysicalFile();

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void FileInfoHelpers_GetInlineData_UTF8()
    {
        // Arrange
        FileInfo file = new()
        {
            Location = "data:text/plain;charset=UTF-8;page=21,the%20data:1234,5678"
        };

        // Act
        string result = file.GetInlineData();

        // Assert
        Assert.That(result, Is.EqualTo("the%20data:12345678"));
    }

    [Test]
    public async Task FileInfoHelpers_GetInlineData_Svg()
    {
        // Arrange
        FileInfo file = new()
        {
            Location = "data:image/svg+xml;utf8,\n<svg width='10' height='10' xmlns='http://www.w3.org/2000/svg'>\n <circle style='fill:red' cx='5' cy='5' r='5'/>\n</svg>"
        };

        // Act
        string result = file.GetInlineData();

        // Assert
        await _context.Verify(result);
    }

    [Test]
    public async Task FileInfoHelpers_GetInlineDataBytes_Base64()
    {
        // Arrange
        FileInfo file = new()
        {
            Location = "data:image/png;base64,iVBORw0KGgoAAA\nANSUhEUgAAAAUAAAAFCAYAAACNbyblAAAAHElEQVQI12P4\n//8/w38GIAXDIBKE0DHxgljNBAAO9TXL0Y4OHwAAAABJRU\n5ErkJggg=="
        };

        // Act
        byte[] result = file.GetInlineDataBytes();

        // Assert
        await _context.Verify(result.Select(x => x.ToString()).Join());
    }


}
