using Microsoft.Extensions.Configuration;
using TestCore.Config;

namespace AppiumTests.Config;

public static class ConfigurationLoader
{
    public static IConfiguration Root { get; } = BuildConfiguration();

    private static IConfiguration BuildConfiguration()
    {
        var env = Environment.GetEnvironmentVariable(EnvVars.TestEnv) ?? "Test";
        return new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: false)
            .AddEnvironmentVariables()
            .Build();
    }

    public static TestEnvironmentOptions GetTestEnvironment() =>
        Root.GetSection(TestEnvironmentOptions.SectionName).Get<TestEnvironmentOptions>() ?? new TestEnvironmentOptions();

    public static BrowserStackOptions GetBrowserStackOptions() =>
        Root.GetSection(BrowserStackOptions.SectionName).Get<BrowserStackOptions>() ?? new BrowserStackOptions();
}
