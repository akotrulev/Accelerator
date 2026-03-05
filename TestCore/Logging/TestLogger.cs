namespace TestCore.Logging;

public enum LogLevel { Info, Warning, Error }

public static class TestLogger
{
    [ThreadStatic]
    private static List<(string Entry, LogLevel Level)>? _logs;

    private static List<(string Entry, LogLevel Level)> Logs => _logs ??= new();

    // Appends a timestamped message to the current thread's log.
    public static void Log(string message, LogLevel level = LogLevel.Info)
    {
        var prefix = level == LogLevel.Info ? "" : $"[{level.ToString().ToUpper()}] ";
        Logs.Add(($"[{DateTime.Now:HH:mm:ss.fff}] {prefix}{message}", level));
    }

    // Discards all log entries for the current thread; call this at the start of each test.
    public static void Clear() => Logs.Clear();

    // Returns a read-only snapshot of all log entries for the current thread.
    public static IReadOnlyList<string> GetLogs() =>
        Logs.Select(l => l.Entry).ToList().AsReadOnly();

    // Returns log entries at or above the given level; pass null to return all entries.
    public static IReadOnlyList<string> GetLogs(LogLevel? minLevel) =>
        minLevel == null
            ? GetLogs()
            : Logs.Where(l => l.Level >= minLevel).Select(l => l.Entry).ToList().AsReadOnly();
}
