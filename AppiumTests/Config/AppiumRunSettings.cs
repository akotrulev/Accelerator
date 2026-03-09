using AppiumTests.Enums;

namespace AppiumTests.Config;

// Single source of truth for Appium execution settings read from environment variables.
public static class AppiumRunSettings
{
    public static ExecutionMode ExecutionMode { get; } =
        Enum.TryParse<ExecutionMode>(Environment.GetEnvironmentVariable(EnvVars.ExecutionMode), true, out var m)
            ? m : ExecutionMode.Local;

    public static MobilePlatform TargetPlatform { get; } =
        Enum.TryParse<MobilePlatform>(Environment.GetEnvironmentVariable(EnvVars.TargetPlatform), out var p)
            ? p
            : MobilePlatform.Android;

    public static TestType TestType { get; } =
        Enum.TryParse<TestType>(Environment.GetEnvironmentVariable(EnvVars.TestType), true, out var t)
            ? t : TestType.Browser;
}
