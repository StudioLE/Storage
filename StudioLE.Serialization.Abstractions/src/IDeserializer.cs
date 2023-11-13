namespace StudioLE.Serialization;

/// <summary>
/// A generic object deserializer.
/// </summary>
public interface IDeserializer
{
    /// <summary>
    /// The file extension of the serialized format.
    /// </summary>
    public string FileExtension { get; }

    /// <summary>
    /// Deserialize the <paramref name="input"/> to <paramref name="type"/>.
    /// </summary>
    /// <param name="input">The serialized input.</param>
    /// <param name="type">The type to deserialize to.</param>
    /// <returns>The deserialized value.</returns>
    public object? Deserialize(TextReader input, Type type);
}
