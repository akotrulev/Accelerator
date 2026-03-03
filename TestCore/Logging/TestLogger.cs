namespace TestCore.Logging;

public static class TestLogger
{
    [ThreadStatic]
    private static List<string>? _logs;

    private static List<string> Logs => _logs ??= new List<string>();

    // Appends a timestamped message to the current thread's log.
    public static void Log(string message) =>
        Logs.Add($"[{DateTime.Now:HH:mm:ss.fff}] {message}");

    // Discards all log entries for the current thread; call this at the start of each test.
    public static void Clear() => Logs.Clear();

    // Returns a read-only snapshot of all log entries for the current thread.
    public static IReadOnlyList<string> GetLogs() => Logs.AsReadOnly();
}
