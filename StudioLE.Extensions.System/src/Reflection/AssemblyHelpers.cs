using System.Diagnostics;
using System.Reflection;

namespace StudioLE.Extensions.System.Reflection;

/// <summary>
/// Methods to help with <see cref="Assembly"/>.
/// </summary>
public static class AssemblyHelpers
{
    private static bool _areAllAssembliesLoaded;

    /// <summary>
    /// Determine if the calling assembly is a DEBUG build.
    /// </summary>
    public static bool IsDebugBuild()
    {
        return Assembly.GetCallingAssembly().IsDebugBuild();
    }

    /// <summary>
    /// Determine if <paramref name="assembly"/> is a DEBUG build.
    /// </summary>
    public static bool IsDebugBuild(this Assembly assembly)
    {
        return assembly
                   .GetCustomAttribute<DebuggableAttribute>()
                   ?.IsJITTrackingEnabled
               ?? false;
    }

    /// <summary>
    /// Get the assembly of the calling methods.
    /// </summary>
    public static IEnumerable<Assembly> GetCallingAssemblies()
    {
        StackFrame[] stackFrames = new StackTrace().GetFrames()
                                   ?? throw new("Failed to get stack trace frames.");
        return stackFrames
            .Select(frame => frame.GetMethod()?.DeclaringType?.Assembly
                             ?? throw new("Failed to get calling assembly of frame."))
            .Skip(1);
    }

    /// <summary>
    /// Ensure all referenced assemblies are loaded.
    /// </summary>
    public static void LoadAllAssemblies(string? whereStartsWith = null)
    {
        if (_areAllAssembliesLoaded)
            return;

        List<Assembly> loadedAssemblies = AppDomain.CurrentDomain
            .GetAssemblies()
            .ToList();

        IEnumerable<AssemblyName> referencedAssemblies = loadedAssemblies
            .SelectMany(x => x.GetReferencedAssemblies())
            .Distinct()
            .Where(x => whereStartsWith == null || x.FullName.StartsWith(whereStartsWith));

        IEnumerable<AssemblyName> notLoadedAssemblies = referencedAssemblies
            .Where(referenced => !loadedAssemblies.Any(loaded => loaded.FullName == referenced.FullName));

        foreach (AssemblyName assembly in notLoadedAssemblies)
            AppDomain.CurrentDomain.Load(assembly);

        _areAllAssembliesLoaded = true;
    }

    /// <summary>
    /// Ensure all referenced assemblies are loaded.
    /// </summary>
    public static Assembly LoadFileByRelativePath(string relativePath)
    {
        FileInfo file = new(relativePath);
        if (!file.Exists)
            throw new($"Failed to load assembly. The file does not exist: {file.FullName}");
        Assembly assembly = Assembly.LoadFile(file.FullName);
        return assembly;
    }
}
