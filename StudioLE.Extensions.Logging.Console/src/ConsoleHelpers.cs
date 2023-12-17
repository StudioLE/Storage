using SystemConsole = System.Console;

namespace StudioLE.Extensions.Logging.Console;

/// <summary>
/// Methods to help with <see cref="SystemConsole"/>.
/// </summary>
public static class ConsoleHelpers
{
    /// <summary>
    /// Capture anything written to the <see cref="SystemConsole"/> standard output when <paramref name="action"/> is executed.
    /// </summary>
    /// <param name="action">The action to capture standard output of.</param>
    /// <returns>The captured output.</returns>
    public static string CaptureConsoleOutput(Action action)
    {
        string output;
        using (StringWriter redirectedOutputWriter = new())
        {
            SystemConsole.SetOut(redirectedOutputWriter);
            action.Invoke();
            output = redirectedOutputWriter.ToString();
        }
        StreamWriter standardOutputWriter = new(SystemConsole.OpenStandardOutput())
        {
            AutoFlush = true
        };
        SystemConsole.SetOut(standardOutputWriter);
        return output;
    }
}
