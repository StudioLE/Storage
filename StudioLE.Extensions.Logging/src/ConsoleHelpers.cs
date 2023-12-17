namespace StudioLE.Extensions.Logging;

/// <summary>
/// Methods to help with <see cref="Console"/>.
/// </summary>
public static class ConsoleHelpers
{
    /// <summary>
    /// Capture anything written to the <see cref="Console"/> standard output when <paramref name="action"/> is executed.
    /// </summary>
    /// <remarks>
    /// This will not capture the output of <c>ConsoleLogger</c> as that runs on a separate thread.
    /// </remarks>
    /// <param name="action">The action to capture standard output of.</param>
    /// <returns>The captured output.</returns>
    public static string CaptureConsoleOutput(Action action)
    {
        string output;
        using (StringWriter redirectedOutputWriter = new())
        {
            Console.SetOut(redirectedOutputWriter);
            action.Invoke();
            output = redirectedOutputWriter.ToString();
        }
        StreamWriter standardOutputWriter = new(Console.OpenStandardOutput())
        {
            AutoFlush = true
        };
        Console.SetOut(standardOutputWriter);
        return output;
    }
}
