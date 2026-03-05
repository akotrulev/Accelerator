using AppiumTests.Enums;

namespace AppiumTests.Config;

// Single source of truth for Appium execution settings read from environment variables.
public static class AppiumRunSettings
{
    public static string ExecutionMode { get; } =
        Environment.GetEnvironmentVariable(EnvVars.ExecutionMode) ?? "Local";

    public static MobilePlatform TargetPlatform { get; } =
        Enum.TryParse<MobilePlatform>(Environment.GetEnvironmentVariable(EnvVars.TargetPlatform), out var p)
            ? p
            : MobilePlatform.Android;

    public static string TestType { get; } =
        Environment.GetEnvironmentVariable(EnvVars.TestType) ?? "Browser";
}
