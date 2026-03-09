using TestCore.Config;

namespace AppiumTests.Config;

// Re-exports shared constants from TestCore; Appium-specific names are kept here.
public static class EnvVars
{
    public const string ExecutionMode  = TestEnvVars.ExecutionMode;
    public const string TargetPlatform = TestEnvVars.TargetPlatform;
    public const string TestType       = TestEnvVars.TestType;
    public const string TestEnv        = TestEnvVars.TestEnv;
    public const string AppPath        = TestEnvVars.AppPath;
    public const string DeviceUdid    = TestEnvVars.DeviceUdid;
    public const string AppPackage    = TestEnvVars.AppPackage;
    public const string AppActivity   = TestEnvVars.AppActivity;
    public const string BundleId      = TestEnvVars.BundleId;
}
