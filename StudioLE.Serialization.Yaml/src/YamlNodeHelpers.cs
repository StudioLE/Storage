using YamlDotNet.RepresentationModel;

namespace StudioLE.Serialization.Yaml;

/// <summary>
/// Methods to help with <see cref="YamlNode"/>.
/// </summary>
public static class YamlNodeHelpers
{
    /// <summary>
    /// Set the value of the node with <paramref name="key"/> in <paramref name="parentNode"/>.
    /// If the key does not exist it will be added.
    /// If the key does exist it will only be set if <paramref name="overwrite"/> is <see langword="true"/>.
    /// </summary>
    /// <param name="parentNode">The parent node to edit.</param>
    /// <param name="key">The key to use.</param>
    /// <param name="value">The value to set.</param>
    /// <param name="overwrite">Should an existing value be overwritten?</param>
    public static void SetValue(this YamlMappingNode parentNode, string key, YamlNode value, bool overwrite = true)
    {
        if (!parentNode.Children.ContainsKey(key))
        {
            parentNode.Add(key, value);
            return;
        }
        if (overwrite)
            return;
        parentNode.Children[key] = value;
    }

    /// <summary>
    /// Get the value of a <see cref="YamlMappingNode"/>.
    /// </summary>
    /// <param name="parentNode">The parent node to retrieve from.</param>
    /// <param name="key">The key to retrieve.</param>
    /// <typeparam name="T">The type of value to retrieve.</typeparam>
    /// <returns>The value, or <see langword="null"/> if the item does not exist, or is not <typeparamref name="T"/>.</returns>
    public static T? GetValue<T>(this YamlMappingNode parentNode, string key)
        where T : YamlNode
    {
        if (parentNode.Children.TryGetValue(key, out YamlNode value))
            return value as T;
        return null;
    }

    /// <summary>
    /// Add a range of values to a <see cref="YamlSequenceNode"/>.
    /// </summary>
    /// <param name="parentNode">The parent to add to.</param>
    /// <param name="nodes">The nodes to add.</param>
    public static void AddRange(this YamlSequenceNode parentNode, IEnumerable<YamlNode> nodes)
    {
        foreach (YamlNode node in nodes)
            parentNode.Add(node);
    }
}
