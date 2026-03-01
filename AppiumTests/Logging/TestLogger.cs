using System.Collections.Generic;

namespace AppiumTests.Logging;

public static class TestLogger
{
    [ThreadStatic]
    private static List<string>? _logs;

    private static List<string> Logs => _logs ??= new List<string>();

    public static void Log(string message) =>
        Logs.Add($"[{DateTime.Now:HH:mm:ss.fff}] {message}");

    public static void Clear() => Logs.Clear();

    public static IReadOnlyList<string> GetLogs() => Logs.AsReadOnly();
}
